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
                .Where(tile => positions.ContainsKey(tile.position))
                .ToDictionary(
                    tile => positions[tile.position],
                    tile => tile.character
                        .GetNeighbors()
                        .Select(offset => offset + tile.position)
                        .Where(map.IsInBounds)
                        .Where(p => map[p].IsFree())
                        .Select(p => positions[p])
                        .ToArray()
                );

            var processedPaths = new HashSet<Node>();

            int count = 0;

            var queue = new Stack<Node>();
            queue.Push(new Node(positions[start]));
            //queue.Enqueue(new Node(positions[start]));

            int[] newNeighbors = new int[4];
            int newNeighborSize = 0;
            while (queue.TryPop(out var node)) {
                var next = new List<int>();
                foreach (int neighborId in neighbors[node.positionId]) {
                    if (!node.IsAncestorOrSelf(neighborId)) {
                        if (neighborId == goalId) {
                            if (count < node.ancestorCount) {
                                count = node.ancestorCount;
                                Console.WriteLine(count);
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
    internal static int positionIdSize;

    internal int positionId;
    internal int ancestorCount;
    internal bool[] path;

    internal Node(int positionId, Node? parent = null) {
        this.positionId = positionId;

        ancestorCount = 1;
        if (parent is not null) {
            ancestorCount += parent.ancestorCount;
        }

        path = new bool[positionIdSize];
        if (parent is not null) {
            Array.Copy(parent.path, path, positionIdSize);
        }

        path[positionId] = true;
    }

    internal Node BecomeChild(int positionId) {
        this.positionId = positionId;
        ancestorCount++;
        path[positionId] = true;
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
        return path[positionId];
    }
}

static class Extensions {
    internal static bool IsFree(this char character) => character != '#';

    internal static Vector2Int[] GetNeighbors(this char character) => character switch {
        '.' => [Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right],
        '^' => [Vector2Int.up],
        'v' => [Vector2Int.down],
        '<' => [Vector2Int.left],
        '>' => [Vector2Int.right],
        _ => [],
    };
}