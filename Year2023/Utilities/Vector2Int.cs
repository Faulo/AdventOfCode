namespace Utilities {
    public sealed record Vector2Int(int x, int y) {
        public static readonly Vector2Int up = new(0, -1);
        public static readonly Vector2Int down = new(0, 1);
        public static readonly Vector2Int left = new(-1, 0);
        public static readonly Vector2Int right = new(1, 0);

        public Vector2Int((int x, int y) position) : this(position.x, position.y) { }

        public int manhattenDistance => x + y;

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y - b.y);
    }
}
