namespace Utilities {
    public struct Vector3Int(long x, long y, long z) {
        public static long DistanceSquared(in Vector3Int left, in Vector3Int right) {
            var delta = right - left;
            return (delta.x * delta.x) + (delta.y * delta.y) + (delta.z * delta.z);
        }

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

        public long x = x;
        public long y = y;
        public long z = z;

        public static Vector3Int operator +(Vector3Int a, Vector3Int b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3Int operator -(Vector3Int a, Vector3Int b) => new(a.x - b.x, a.y - b.y, a.z - b.z);

        public static bool operator ==(Vector3Int a, Vector3Int b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(Vector3Int a, Vector3Int b) => a.x != b.x || a.y != b.y || a.z != b.z;

        public override readonly bool Equals(object? obj) {
            return obj is Vector3Int other && other == this;
        }

        public override readonly int GetHashCode() {
            return (int)((x << 24) | (y << 12) | z);
        }

        public override readonly string ToString() => $"({x}, {y}, {z})";

        public readonly Vector3Int WithX(long x) => new(x, y, z);
        public readonly Vector3Int WithY(long y) => new(x, y, z);
        public readonly Vector3Int WithZ(long z) => new(x, y, z);
    }
}
