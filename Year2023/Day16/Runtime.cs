using Utilities;

namespace Day16;

sealed class Runtime(string file) {
    CharacterMap map = new FileInput(file).ReadAllAsCharacterMap();

    internal int numberOfEnergizedTiles {
        get {
            var energized = new Directions[map.width, map.height];

            Move(energized, new(0, 0), Directions.Right);

            return energized
                .AsEnumerable()
                .Count(d => d != Directions.None);
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
[Flags]
public enum Directions {
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
}

static class Extensions {
    internal static IEnumerable<T> AsEnumerable<T>(this T[,] map) {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                yield return map[x, y];
            }
        }
    }
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