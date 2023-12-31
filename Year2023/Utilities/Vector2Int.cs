namespace Utilities {
    public sealed record Vector2Int(int x, int y) {
        /// <summary>
        /// (0, 0)
        /// </summary>
        public static readonly Vector2Int zero = new(0, 0);

        /// <summary>
        /// (0, -1)
        /// </summary>
        public static readonly Vector2Int up = new(0, -1);

        /// <summary>
        /// (0, 1)
        /// </summary>
        public static readonly Vector2Int down = new(0, 1);

        /// <summary>
        /// (-1, 0)
        /// </summary>
        public static readonly Vector2Int left = new(-1, 0);

        /// <summary>
        /// (1, 0)
        /// </summary>
        public static readonly Vector2Int right = new(1, 0);

        public Vector2Int((int x, int y) position) : this(position.x, position.y) { }

        public int manhattenDistance => x + y;

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y - b.y);

        public static Vector2Int operator *(Vector2Int a, int b) => new(a.x * b, a.y * b);
        public static Vector2Int operator *(int b, Vector2Int a) => new(a.x * b, a.y * b);
    }
}
