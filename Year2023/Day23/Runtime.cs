using System.Buffers;
using System.Runtime.CompilerServices;
using Utilities;

namespace Day23;

sealed class Runtime {
    static readonly bool useMultithreading = true;

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

            if (useMultithreading) {
                int seedCount = 8 * Environment.ProcessorCount;

                var queue = new Queue<Node>();

                queue.Enqueue(startNode);

                while (queue.TryDequeue(out var node)) {
                    ProcessNode(pool, node, queue.Enqueue, ref count);
                    if (queue.Count >= seedCount) {
                        break;
                    }
                }

                count = queue.AsParallel()
                    .Select(seedNode => {
                        var pool = new NodePool();
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
            } else {
                var queue = new Stack<Node>();

                queue.Push(startNode);

                while (queue.TryPop(out var node)) {
                    ProcessNode(pool, node, queue.Push, ref count);
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
                    int newNeighborSize = 0;

                    foreach (int neighborId in nodeNeighbors) {
                        if (!node.IsAncestorOrSelf(neighborId)) {
                            pool.tempNeighbors[newNeighborSize++] = neighborId;
                        }
                    }

                    if (newNeighborSize == 0) {
                        // dead end
                        pool.Return(node);
                        return;
                    }

                    while (newNeighborSize > 1) {
                        int neighborId = pool.tempNeighbors[--newNeighborSize];
                        var child = pool.Rent().Init(neighborId, node);
                        process(child);
                    }

                    node.BecomeChild(pool.tempNeighbors[0]);

                    break;
                }
            }
        }

        // goal reached!
        if (count < node.ancestorCount) {
            count = node.ancestorCount;
        }
    }
}

sealed class NodePool {
    readonly Stack<Node> pool = new();

    internal Node Rent() {
        return pool.TryPop(out var node)
            ? node
            : new Node();
    }

    internal void Return(Node node) {
        pool.Push(node);
    }

    internal readonly int[] tempNeighbors = new int[4];
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