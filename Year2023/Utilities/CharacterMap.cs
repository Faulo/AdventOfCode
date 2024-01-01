using System.Text;

namespace Utilities {
    public sealed record CharacterMap(char[,] map) {
        public char this[Vector2Int position] {
            get {
                return map[position.x, position.y];
            }
            set {
                map[position.x, position.y] = value;
            }
        }
        public char this[(int x, int y) position] {
            get {
                return map[position.x, position.y];
            }
            set {
                map[position.x, position.y] = value;
            }
        }
        public char this[int x, int y] {
            get {
                return map[x, y];
            }
            set {
                map[x, y] = value;
            }
        }
        public readonly int width = map.GetLength(0);
        public readonly int height = map.GetLength(1);

        public IEnumerable<Vector2Int> allPositionsWithin => Enumerable.Range(0, width)
            .SelectMany(x => Enumerable.Range(0, height).Select(y => (x, y)))
            .Select(p => new Vector2Int(p));

        public IEnumerable<(Vector2Int position, char character)> allPositionsAndCharactersWithin => allPositionsWithin
            .Select(position => (position, this[position]));

        public bool IsInBounds(Vector2Int position) {
            return position.x >= 0 && position.x < width
                && position.y >= 0 && position.y < height;
        }

        public string Serialize() {
            var builder = new StringBuilder();
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    builder.Append(this[x, y]);
                }
            }

            return builder.ToString();
        }

        public void Deserialize(string cache) {
            int i = 0;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    this[x, y] = cache[i];
                    i++;
                }
            }
        }

        public long FloodFill(Vector2Int position, char from, char to) {
            long sum = 0;

            var queue = new HashSet<Vector2Int> {
                position
            };

            do {
                position = queue.First();
                queue.Remove(position);

                if (this[position] == from) {
                    this[position] = to;
                    sum++;
                    foreach (var offset in offsets) {
                        var p = position + offset;
                        if (IsInBounds(p) && this[p] == from) {
                            queue.Add(p);
                        }
                    }
                }
            } while (queue.Count > 0);

            return sum;
        }

        static readonly Vector2Int[] offsets = [
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
        ];
    }
}
