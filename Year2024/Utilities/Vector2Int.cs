namespace Utilities {
    public readonly struct Vector2Int {
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

        public readonly int x;
        public readonly int y;
        public readonly long x64;
        public readonly long y64;

        public Vector2Int((int x, int y) position) : this(position.x, position.y) { }

        public Vector2Int(int x, int y) {
            this.x = x;
            this.y = y;
            x64 = x;
            y64 = y;
        }

        public Vector2Int(long x, long y) {
            this.x = (int)x;
            this.y = (int)y;
            x64 = x;
            y64 = y;
        }

        public long manhattenDistance => x64 + y64;

        public IEnumerable<Vector2Int> neighbors => new Vector2Int[] {
            new(x, y-1),
            new(x, y+1),
            new(x-1, y),
            new(x+1, y),
        };

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y - b.y);

        public static Vector2Int operator *(Vector2Int a, int b) => new(a.x * b, a.y * b);
        public static Vector2Int operator *(Vector2Int a, long b) => new(a.x64 * b, a.y64 * b);
        public static Vector2Int operator *(int b, Vector2Int a) => new(a.x * b, a.y * b);
        public static Vector2Int operator *(long b, Vector2Int a) => new(a.x64 * b, a.y64 * b);

        public static Vector2Int operator /(Vector2Int a, int b) => new(a.x / b, a.y / b);
        public static Vector2Int operator /(Vector2Int a, long b) => new(a.x64 / b, a.y64 / b);

        public static bool operator ==(Vector2Int a, Vector2Int b) => a.x64 == b.x64 && a.y64 == b.y64;
        public static bool operator !=(Vector2Int a, Vector2Int b) => a.x64 != b.x64 || a.y64 != b.y64;

        public override bool Equals(object? obj) {
            return obj is Vector2Int other && other == this;
        }

        public override int GetHashCode() {
            return (x << 16) | y;
        }

        public override string ToString() => $"({x64}, {y64})";
    }
}
