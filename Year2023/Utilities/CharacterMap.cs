namespace Utilities {
    public sealed record CharacterMap(char[,] map) {
        public char this[Vector2Int position] {
            get {
                return map[position.x, position.y];
            }
        }
        public readonly int width = map.GetLength(0);
        public readonly int height = map.GetLength(1);

        public IEnumerable<Vector2Int> allPositionsWithin => Enumerable.Range(0, width)
            .Zip(Enumerable.Range(0, height))
            .Select(p => new Vector2Int(p));

        public IEnumerable<(Vector2Int, char)> allPositionsAndCharactersWithin => allPositionsWithin
            .Select(position => (position, this[position]));
    }
}
