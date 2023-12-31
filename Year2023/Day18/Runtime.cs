using Utilities;

namespace Day18;

sealed class Runtime {
    internal int totalDigArea {
        get {
            return 0;
        }
    }

    internal Runtime(string file) {
        new FileInput(file);
    }
}

[Flags]
public enum Directions {
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    All = Up | Down | Left | Right,
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
    internal static int AsInteger(this char character) {
        return character - '0';
    }
    internal static Directions GetOpposite(this Directions direction) {
        return direction switch {
            Directions.Up => Directions.Down,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Right,
            Directions.Right => Directions.Left,
            _ => Directions.None,
        };
    }
    internal static Vector2Int GetOffset(this Directions direction) {
        return direction switch {
            Directions.Up => Vector2Int.up,
            Directions.Down => Vector2Int.down,
            Directions.Left => Vector2Int.left,
            Directions.Right => Vector2Int.right,
            _ => new(0, 0),
        };
    }
    internal static string ToCharacter(this Directions direction) {
        return direction switch {
            Directions.Up => "^",
            Directions.Down => "v",
            Directions.Left => "<",
            Directions.Right => ">",
            _ => "o",
        };
    }
}