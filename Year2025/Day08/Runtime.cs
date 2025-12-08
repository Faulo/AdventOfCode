using Utilities;

namespace Day08;

sealed partial class Runtime {
    readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
    }

    Vector2Int start => map
        .allPositionsAndCharactersWithin
        .First(tile => tile.character == 'S')
        .position;

    internal int splitCount {
        get {
            var dict = new Dictionary<Vector2Int, bool> {
                [start] = true
            };

            int count = 0;

            for (int y  = start.y + 1; y < map.height; y++) { }

            Console.WriteLine(start);

            foreach (var (position, character) in map.allPositionsAndCharactersWithin) {
                if (character == '^') {
                }
            }

            return count;
        }
    }

    internal long allFreshIngredients => 0;
}