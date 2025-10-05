namespace Utilities;

[Flags]
public enum EDirections : byte {
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    All = Up | Down | Left | Right,
}

public static class DirectionsExtensions {
    public static EDirections GetOpposite(this EDirections direction) {
        return direction switch {
            EDirections.Up => EDirections.Down,
            EDirections.Down => EDirections.Up,
            EDirections.Left => EDirections.Right,
            EDirections.Right => EDirections.Left,
            _ => EDirections.None,
        };
    }
    public static Vector2Int GetOffset(this EDirections direction) {
        return direction switch {
            EDirections.Up => Vector2Int.up,
            EDirections.Down => Vector2Int.down,
            EDirections.Left => Vector2Int.left,
            EDirections.Right => Vector2Int.right,
            _ => new(0, 0),
        };
    }
    public static string ToCharacter(this EDirections direction) {
        return direction switch {
            EDirections.Up => "^",
            EDirections.Down => "v",
            EDirections.Left => "<",
            EDirections.Right => ">",
            _ => "o",
        };
    }
}