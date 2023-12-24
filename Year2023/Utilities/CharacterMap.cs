namespace Utilities {
    public sealed record CharacterMap(char[,] map) {
        public char this[Vector2Int position] {
            get {
                return map[position.x, position.y];
            }
        }
        public char this[(int x, int y) position] {
            get {
                return map[position.x, position.y];
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
    }
}
