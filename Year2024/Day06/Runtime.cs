using Utilities;

namespace Day06;

sealed partial class Runtime {

    internal readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
    }

    static readonly Dictionary<char, Vector2Int> directions = new() {
        ['^'] = Vector2Int.up,
        ['v'] = Vector2Int.down,
        ['<'] = Vector2Int.left,
        ['>'] = Vector2Int.right,

    };

    static readonly Dictionary<char, char> ninetyDegreesRight = new() {
        ['^'] = '>',
        ['v'] = '<',
        ['<'] = '^',
        ['>'] = 'v',
    };

    int CalculatePath(Vector2Int position, char direction) {
        var positions = new HashSet<Vector2Int>();

        while (map.IsInBounds(position)) {
            positions.Add(position);

            while (map.TryGet(position + directions[direction], out char tile) && tile == '#') {
                direction = ninetyDegreesRight[direction];
            }

            position += directions[direction];
        }

        return positions.Count;
    }

    internal int pathCount {
        get {
            var (position, character) = map
                .allPositionsAndCharactersWithin
                .First(tile => directions.ContainsKey(tile.character));

            return CalculatePath(position, character);
        }
    }
}