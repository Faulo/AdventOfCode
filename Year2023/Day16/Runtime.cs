using System.Collections.Concurrent;
using Utilities;

namespace Day16;

sealed class Runtime(string file) {
    CharacterMap map = new FileInput(file).ReadAllAsCharacterMap();

    internal int numberOfEnergizedTiles => CountEnergizedTiels(new(0, 0), Directions.Right);

    int CountEnergizedTiels(Vector2Int position, Directions direction) {
        var energized = new Directions[map.width, map.height];

        Move(energized, position, direction);

        return energized
            .AsEnumerable()
            .Count(d => d != Directions.None);
    }

    internal int maximumNumberOfEnergizedTiles {
        get {
            var counts = new ConcurrentStack<int>();

            Parallel.ForEach(startingPositions, start => counts.Push(CountEnergizedTiels(start.position, start.direction)));

            return counts.Max();
        }
    }

    IEnumerable<(Vector2Int position, Directions direction)> startingPositions {
        get {
            for (int x = 0; x < map.width; x++) {
                yield return (new(x, 0), Directions.Down);
                yield return (new(x, map.height - 1), Directions.Up);
            }

            for (int y = 0; y < map.height; y++) {
                yield return (new(0, y), Directions.Right);
                yield return (new(map.width - 1, y), Directions.Left);
            }
        }
    }

    void Move(Directions[,] energized, Vector2Int position, Directions direction) {
        if (!map.IsInBounds(position)) {
            return;
        }

        if (energized[position.x, position.y].HasFlag(direction)) {
            return;
        }

        energized[position.x, position.y] |= direction;

        var next = map[position].GetDirections(direction);

        if (next.HasFlag(Directions.Up)) {
            Move(energized, position + Vector2Int.up, Directions.Up);
        }

        if (next.HasFlag(Directions.Down)) {
            Move(energized, position + Vector2Int.down, Directions.Down);
        }

        if (next.HasFlag(Directions.Left)) {
            Move(energized, position + Vector2Int.left, Directions.Left);
        }

        if (next.HasFlag(Directions.Right)) {
            Move(energized, position + Vector2Int.right, Directions.Right);
        }
    }
}

static class Extensions {
    internal static Directions GetDirections(this char tile, Directions source) {
        return tile switch {
            '.' => source,
            '-' => source switch {
                Directions.Up => Directions.Left | Directions.Right,
                Directions.Down => Directions.Left | Directions.Right,
                _ => source,
            },
            '|' => source switch {
                Directions.Left => Directions.Up | Directions.Down,
                Directions.Right => Directions.Up | Directions.Down,
                _ => source,
            },
            '\\' => source switch {
                Directions.Up => Directions.Left,
                Directions.Down => Directions.Right,
                Directions.Left => Directions.Up,
                Directions.Right => Directions.Down,
                _ => Directions.None,
            },
            '/' => source switch {
                Directions.Up => Directions.Right,
                Directions.Down => Directions.Left,
                Directions.Left => Directions.Down,
                Directions.Right => Directions.Up,
                _ => Directions.None,
            },
            _ => Directions.None,
        };
    }
}