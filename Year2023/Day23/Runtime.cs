using System.Buffers;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Utilities;

namespace Day23;

sealed class Runtime {
    enum ThreadingMode {
        SingleThreadedStack,
        SingleThreadedRecursion,
        PLINQ,
        TasksWithChannel,
        TasksWithStack,
    }

    static readonly ThreadingMode threading = ThreadingMode.TasksWithStack;

    sealed class TaskContext {
        internal readonly Channel<Node> channel = Channel.CreateUnbounded<Node>(
            new UnboundedChannelOptions {
                SingleReader = false,
                SingleWriter = false,
                AllowSynchronousContinuations = false,
            }
        );
        internal readonly ConcurrentStack<Node> queue = new();
        internal bool isDone;

        internal required Runtime runtime;
        internal required NodePool pool;
    }

    readonly CharacterMap map;
    internal readonly Vector2Int start;
    internal readonly Vector2Int goal;

    readonly Dictionary<Vector2Int, int> positions;
    readonly int goalId;
    readonly int[][] neighbors;

    internal Runtime(string file, bool replaceSlopes = false) {
        map = new FileInput(file).ReadAllAsCharacterMap();

        if (replaceSlopes) {
            foreach (var (position, character) in map.allPositionsAndCharactersWithin) {
                if (character.IsFree()) {
                    map[position] = '.';
                }
            }
        }

        start = map
            .allPositionsAndCharactersWithin
            .First(tile => tile.character.IsFree())
            .position;

        goal = map
            .allPositionsAndCharactersWithin
            .Last(tile => tile.character.IsFree())
            .position;

        positions = [];
        int i = 0;
        foreach (var (position, _) in map.allPositionsAndCharactersWithin.Where(tile => tile.character.IsFree())) {
            positions[position] = i++;
        }

        Node.positionIdSize = i;

        goalId = positions[goal];

        neighbors = [.. map
            .allPositionsAndCharactersWithin
            .Where(tile => tile.character.IsFree())
            .Select(tile => tile.character
                .GetNeighbors()
                .Select(offset => offset + tile.position)
                .Where(map.IsInBounds)
                .Where(p => map[p].IsFree())
                .Select(p => positions[p])
                .ToArray()
            )
        ];
    }

    internal int maximumNumberOfSteps {
        get {
            int count = 0;
            var pool = new NodePool();
            var startNode = pool
                .Rent()
                .Init(positions[start]);

            switch (threading) {
                case ThreadingMode.SingleThreadedStack: {
                    var queue = new Stack<Node>();

                    queue.Push(startNode);

                    while (queue.TryPop(out var node)) {
                        ProcessNode(pool, node, queue.Push, ref count);
                    }

                    break;
                }
                case ThreadingMode.SingleThreadedRecursion: {
                    void recurse(Node node) {
                        ProcessNode(pool, node, recurse, ref count);
                    }

                    recurse(startNode);
                    break;
                }
                case ThreadingMode.PLINQ: {
                    int seedCount = 8 * Environment.ProcessorCount;

                    var queue = new Queue<Node>();

                    queue.Enqueue(startNode);

                    while (queue.TryDequeue(out var node)) {
                        ProcessNode(pool, node, queue.Enqueue, ref count);
                        if (queue.Count >= seedCount) {
                            break;
                        }
                    }

                    count = queue
                        .AsParallel()
                        .Select(seedNode => {
                            var queue = new Stack<Node>();

                            queue.Push(seedNode);

                            int count = 0;

                            while (queue.TryPop(out var node)) {
                                ProcessNode(pool, node, queue.Push, ref count);
                            }

                            return count;
                        })
                        .Append(count)
                        .Max();

                    break;
                }
                case ThreadingMode.TasksWithChannel: {
                    int threadCount = Environment.ProcessorCount;

                    var context = new TaskContext {
                        runtime = this,
                        pool = pool,
                    };

                    context.channel.Writer.TryWrite(startNode);

                    pool.onReturnLast = () => context.channel.Writer.Complete();

                    static async Task<int> work(TaskContext context) {
                        await Task.Yield();

                        int count = 0;

                        await foreach (var node in context.channel.Reader.ReadAllAsync()) {
                            context.runtime.ProcessNode(context.pool, node, async child => await context.channel.Writer.WriteAsync(child), ref count);
                        }

                        return count;
                    }

                    count = Task
                        .WhenAll(Enumerable.Range(0, threadCount).Select(_ => work(context)))
                        .Result
                        .Max(count => count);

                    break;
                }
                case ThreadingMode.TasksWithStack: {
                    int threadCount = Environment.ProcessorCount;

                    var context = new TaskContext {
                        runtime = this,
                        pool = pool,
                    };

                    context.queue.Push(startNode);

                    pool.onReturnLast = () => context.isDone = true;

                    static async Task<int> work(TaskContext context) {
                        await Task.Yield();

                        int count = 0;

                        while (!context.isDone) {
                            if (context.queue.TryPop(out var node)) {
                                context.runtime.ProcessNode(context.pool, node, context.queue.Push, ref count);
                            } else {
                                await Task.Yield();
                            }
                        }

                        return count;
                    }

                    count = Task
                        .WhenAll(Enumerable.Range(0, threadCount).Select(_ => work(context)))
                        .Result
                        .Max(count => count);

                    break;
                }
            }

            return count;
        }
    }

    void ProcessNode(NodePool pool, Node node, Action<Node> process, ref int count) {
        while (node.positionId != goalId) {
            int[] nodeNeighbors = neighbors[node.positionId];

            switch (nodeNeighbors.Length) {
                // exactly one neighbor means we go there
                case 1: {
                    if (node.IsAncestorOrSelf(nodeNeighbors[0])) {
                        // dead end
                        pool.Return(node);
                        return;
                    }

                    node.BecomeChild(nodeNeighbors[0]);

                    break;
                }
                // two neighbors means we go there
                case 2: {
                    bool firstIsPartOfPath = node.IsAncestorOrSelf(nodeNeighbors[0]);
                    bool secondIsPartOfPath = node.IsAncestorOrSelf(nodeNeighbors[1]);

                    if (firstIsPartOfPath == secondIsPartOfPath) {
                        // dead end
                        pool.Return(node);
                        return;
                    }

                    int neighborId = nodeNeighbors[firstIsPartOfPath ? 1 : 0];

                    node.BecomeChild(neighborId);

                    break;
                }
                default: {
                    int neighborsMask = 0;

                    for (int i = 0; i < nodeNeighbors.Length; i++) {
                        if (!node.IsAncestorOrSelf(nodeNeighbors[i])) {
                            neighborsMask |= 1 << i;
                        }
                    }

                    if (neighborsMask == 0) {
                        // dead end
                        pool.Return(node);
                        return;
                    }

                    for (int i = 0; i < nodeNeighbors.Length; i++) {
                        if (neighborsMask == 1) {
                            node.BecomeChild(nodeNeighbors[i]);
                            break;
                        }

                        if ((neighborsMask & 1) == 1) {
                            int neighborId = nodeNeighbors[i];
                            var child = pool.Rent().Init(neighborId, node);
                            process(child);
                        }

                        neighborsMask >>= 1;
                    }

                    break;
                }
            }
        }

        // goal reached!
        if (count < node.ancestorCount) {
            count = node.ancestorCount;
        }

        pool.Return(node);
    }
}

sealed class NodePool {
    readonly ConcurrentStack<Node> pool = new();

    internal Action? onReturnLast;

    int rentedCount = 0;

    internal bool hasRentedNodes => rentedCount > 0;

    internal Node Rent() {
        Interlocked.Increment(ref rentedCount);
        return pool.TryPop(out var node)
            ? node
            : new Node();
    }

    internal void Return(Node node) {
        pool.Push(node);
        if (Interlocked.Decrement(ref rentedCount) == 0) {
            if (onReturnLast is not null) {
                onReturnLast();
                onReturnLast = null;
            }
        }
    }
}

sealed class Node {
    internal static int positionIdSize {
        set {
#if USE_LONG
            pathMaxSize = 1 + ((value - 1) >> 6); // ceil(n/64)
#else
            pathMaxSize = value;
#endif
        }
    }
    static int pathMaxSize;

    internal int positionId;
    internal int ancestorCount;

#if USE_LONG
    internal ulong[] path = new ulong[pathMaxSize];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static (int word, ulong bit) SplitId(int id) {
        int word = id >> 6;
        int bit = id & 63;
        return (word, 1UL << bit);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetPath(int id) {
        (int word, ulong bit) = SplitId(id);
        path[word] |= bit;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool GetPath(int id) {
        (int word, ulong bit) = SplitId(id);
        return (path[word] & bit) != 0;
    }
#else
    internal bool[] path = new bool[pathMaxSize];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SetPath(int id) {
        path[id] = true;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool GetPath(int id) {
        return path[id];
    }
#endif

    internal Node() {
    }
    internal Node(int positionId) {
        Init(positionId);
    }
    internal Node(int positionId, Node parent) {
        Init(positionId, parent);
    }

    internal Node Init(int positionId) {
        this.positionId = positionId;

        ancestorCount = 0;

        SetPath(positionId);

        return this;
    }

    internal Node Init(int positionId, Node parent) {
        this.positionId = positionId;

        ancestorCount = 1 + parent.ancestorCount;

        Array.Copy(parent.path, path, pathMaxSize);

        SetPath(positionId);

        return this;
    }

    internal Node BecomeChild(int positionId) {
        this.positionId = positionId;
        ancestorCount++;
        SetPath(positionId);
        return this;
    }

    internal bool IsAncestorOrSelf(int positionId) {
        return GetPath(positionId);
    }
}

static class Extensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsFree(this char character) => character is not '#';

    internal static Vector2Int[] GetNeighbors(this char character) => character switch {
        '.' => _all4,
        '^' => _up,
        'v' => _down,
        '<' => _left,
        '>' => _right,
        _ => _none,
    };

    static readonly Vector2Int[] _none = [];
    static readonly Vector2Int[] _up = [Vector2Int.up];
    static readonly Vector2Int[] _down = [Vector2Int.down];
    static readonly Vector2Int[] _left = [Vector2Int.left];
    static readonly Vector2Int[] _right = [Vector2Int.right];
    static readonly Vector2Int[] _all4 = [Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right];
}