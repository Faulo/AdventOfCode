using System.Runtime.CompilerServices;
using Utilities;

namespace Day23;

sealed class Runtime {
    internal int maximumNumberOfSteps {
        get {
            var positions = new Dictionary<Vector2Int, int>();
            int i = 0;
            foreach (var (position, _) in map.allPositionsAndCharactersWithin.Where(tile => tile.character.IsFree())) {
                positions[position] = i++;
            }

            Node.positionIdSize = i;

            int goalId = positions[goal];

            var neighbors = map
                .allPositionsAndCharactersWithin
                .Where(tile => tile.character.IsFree())
                .Select(tile => tile.character
                    .GetNeighbors()
                    .Select(offset => offset + tile.position)
                    .Where(map.IsInBounds)
                    .Where(p => map[p].IsFree())
                    .Select(p => positions[p])
                    .ToArray())
                .ToArray()
                .AsSpan();

            int count = 0;

            var queue = new Stack<Node>();
            queue.Push(new Node(positions[start]));

            var newNeighbors = new int[4].AsSpan();
            int newNeighborSize = 0;
            while (queue.TryPop(out var node)) {
                var next = new List<int>();
                foreach (int neighborId in neighbors[node.positionId]) {
                    if (!node.IsAncestorOrSelf(neighborId)) {
                        if (neighborId == goalId) {
                            if (count < node.ancestorCount) {
                                count = node.ancestorCount;
                            }
                        } else {
                            newNeighbors[newNeighborSize++] = neighborId;
                        }
                    }
                }

                while (newNeighborSize > 0) {
                    int neighborId = newNeighbors[--newNeighborSize];
                    var child = newNeighborSize == 0
                        ? node.BecomeChild(neighborId)
                        : node.CreateChild(neighborId);
                    queue.Push(child);
                }
            }

            return count;
        }
    }

    readonly CharacterMap map;
    internal readonly Vector2Int start;
    internal readonly Vector2Int goal;

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
    }
}

sealed class Node {
    internal static int positionIdSize {
        set {
            pathMaxSize = 1 + ((value - 1) >> 6); // ceil(n/64)
        }
    }
    static int pathMaxSize;

    internal int positionId;
    internal int ancestorCount;
    internal ulong[] path;

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

    internal Node(int positionId, Node? parent = null) {
        this.positionId = positionId;

        ancestorCount = 1;
        if (parent is not null) {
            ancestorCount += parent.ancestorCount;
        }

        path = new ulong[pathMaxSize];
        if (parent is not null) {
            Array.Copy(parent.path, path, pathMaxSize);
        }

        SetPath(positionId);
    }

    internal Node BecomeChild(int positionId) {
        this.positionId = positionId;
        ancestorCount++;
        SetPath(positionId);
        return this;
    }

    internal Node CreateChild(int positionId) {
        return new Node(positionId, this);
    }

    public override int GetHashCode() => ancestorCount;

    public override bool Equals(object? obj) {
        return obj is Node node && path.AsSpan().SequenceEqual(node.path);
    }

    internal bool IsAncestorOrSelf(int positionId) {
        return GetPath(positionId);
    }
}

static class Extensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsFree(this char character) => character != '#';

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