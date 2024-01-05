using Utilities;

namespace Day23;

sealed class Runtime {
    internal int maximumNumberOfSteps {
        get {
            var neighbors = map
                .allPositionsAndCharactersWithin
                .ToDictionary(
                    tile => tile.position,
                    tile => tile.character
                        .GetNeighbors()
                        .Select(offset => offset + tile.position)
                        .Where(map.IsInBounds)
                        .Where(p => map[p].IsFree())
                        .ToArray()
                );

            int count = 0;

            var queue = new Queue<Node>();
            queue.Enqueue(new Node(start));

            while (queue.TryDequeue(out var node)) {
                foreach (var neighbor in neighbors[node.position]) {
                    if (!node.IsAncestorOrSelf(neighbor)) {
                        if (neighbor == goal) {
                            count = Math.Max(count, node.ancestorCount);
                        } else {
                            queue.Enqueue(new Node(neighbor, node));
                        }
                    }
                }
            }

            return count + 1;
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
    internal readonly Vector2Int position;
    readonly Node? parent;
    internal int ancestorCount {
        get {
            int i = 0;
            for (var node = parent; node is not null; node = node.parent) {
                i++;
            }

            return i;
        }
    }

    internal Node(Vector2Int position, Node? parent = null) {
        this.position = position;
        this.parent = parent;
    }

    internal bool IsAncestorOrSelf(Vector2Int position) {
        for (var node = this; node is not null; node = node.parent) {
            if (node.position == position) {
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