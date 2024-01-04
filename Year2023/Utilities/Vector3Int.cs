namespace Utilities {
    public struct Vector3Int {
        /// <summary>
        /// (0, 0, 0)
        /// </summary>
        public static readonly Vector3Int zero = new(0, 0, 0);

        /// <summary>
        /// (0, 0, 1)
        /// </summary>
        public static readonly Vector3Int up = new(0, 0, 1);

        /// <summary>
        /// (0, 0, -1)
        /// </summary>
        public static readonly Vector3Int down = new(0, 0, -1);

        public int x;
        public int y;
        public int z;

        public Vector3Int(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3Int operator +(Vector3Int a, Vector3Int b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3Int operator -(Vector3Int a, Vector3Int b) => new(a.x - b.x, a.y - b.y, a.z - b.z);

        public static bool operator ==(Vector3Int a, Vector3Int b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(Vector3Int a, Vector3Int b) => a.x != b.x || a.y != b.y || a.z != b.z;

        public override bool Equals(object? obj) {
            return obj is Vector3Int other && other == this;
        }

        public override int GetHashCode() {
            return (x << 24) | (y << 12) | z;
        }

        public override string ToString() => $"({x}, {y}, {z})";

        public Vector3Int WithX(int x) => new(x, y, z);
        public Vector3Int WithY(int y) => new(x, y, z);
        public Vector3Int WithZ(int z) => new(x, y, z);
    }
}
