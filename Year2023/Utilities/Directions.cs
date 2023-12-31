namespace Utilities;

[Flags]
public enum Directions {
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    All = Up | Down | Left | Right,
}

public static class DirectionsExtensions {
    public static Directions GetOpposite(this Directions direction) {
        return direction switch {
            Directions.Up => Directions.Down,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Right,
            Directions.Right => Directions.Left,
            _ => Directions.None,
        };
    }
    public static Vector2Int GetOffset(this Directions direction) {
        return direction switch {
            Directions.Up => Vector2Int.up,
            Directions.Down => Vector2Int.down,
            Directions.Left => Vector2Int.left,
            Directions.Right => Vector2Int.right,
            _ => new(0, 0),
        };
    }
    public static string ToCharacter(this Directions direction) {
        return direction switch {
            Directions.Up => "^",
            Directions.Down => "v",
            Directions.Left => "<",
            Directions.Right => ">",
            _ => "o",
        };
    }
}