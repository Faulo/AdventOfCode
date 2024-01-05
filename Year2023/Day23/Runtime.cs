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

            var processedPaths = new HashSet<string>();

            int count = 0;

            var queue = new Stack<Node>();
            queue.Push(new Node(positions[start]));

            while (queue.TryPop(out var node)) {
                if (!processedPaths.Add(node.hash)) {
                    continue;
                }

                foreach (int neighborId in neighbors[node.positionId]) {
                    if (!node.IsAncestorOrSelf(neighborId)) {
                        if (neighborId == goalId) {
                            count = Math.Max(count, node.ancestorCount);
                            Console.WriteLine(count);
                        } else {
                            var child = new Node(neighborId, node);
                            if (!processedPaths.Contains(child.hash)) {
                                queue.Push(child);
                            }
                        }
                    }
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

    readonly Node? parent;

    internal readonly int positionId;
    internal readonly char[] hashes;
    internal readonly string hash;

    internal int ancestorCount;

    internal Node(int positionId, Node? parent = null) {
        this.positionId = positionId;
        this.parent = parent;

        ancestorCount = 1;

        hashes = new char[positionIdSize];

        if (parent is not null) {
            ancestorCount += parent.ancestorCount;
            Array.Copy(parent.hashes, hashes, positionIdSize);
        }

        hashes[positionId] = '.';
        hash = new(hashes);
    }

    public override int GetHashCode() => ancestorCount;

    public override bool Equals(object? obj) {
        if (obj is Node node) {
            for (int i = 0; i < positionIdSize; i++) {
                if (hashes[i] != node.hashes[i]) {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    internal bool IsAncestorOrSelf(int positionId) {
        for (var node = this; node is not null; node = node.parent) {
            if (node.positionId == positionId) {
                return true;
            }
        }

        return false;
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