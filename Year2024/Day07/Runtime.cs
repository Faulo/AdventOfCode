using Utilities;

namespace Day07;

sealed partial class Runtime {

    internal readonly CharacterMap map;

    readonly HashSet<Vector2Int> wallPositions;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();

        wallPositions = map
            .allPositionsAndCharactersWithin
            .Where(tile => tile.character == '#')
            .Select(tile => tile.position)
            .ToHashSet();
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

    static readonly Dictionary<char, char> ninetyDegreesLeft = new() {
        ['^'] = '<',
        ['v'] = '>',
        ['<'] = 'v',
        ['>'] = '^',
    };

    HashSet<Vector2Int> CalculatePath(Vector2Int position, char direction) {
        var positions = new HashSet<Vector2Int>();

        while (map.IsInBounds(position)) {
            positions.Add(position);

            while (wallPositions.Contains(position + directions[direction])) {
                direction = ninetyDegreesRight[direction];
            }

            position += directions[direction];
        }

        return positions;
    }

    bool IsLoop(Vector2Int position, char direction, Vector2Int wallPosition) {
        var positions = new HashSet<Vector3Int>();

        for (; positions.Add(new(position.x, position.y, direction));) {
            var nextPosition = position + directions[direction];

            if (!map.IsInBounds(nextPosition)) {
                return false;
            }

            if (nextPosition == wallPosition || wallPositions.Contains(nextPosition)) {
                direction = ninetyDegreesRight[direction];
            } else {
                position += directions[direction];
            }
        }

        return true;
    }

    internal int pathCount {
        get {
            var (position, character) = map
                .allPositionsAndCharactersWithin
                .First(tile => directions.ContainsKey(tile.character));

            return CalculatePath(position, character).Count;
        }
    }

    internal int obstructionCount {
        get {
            var (position, direction) = map
                .allPositionsAndCharactersWithin
                .First(tile => directions.ContainsKey(tile.character));

            return CalculatePath(position, direction)
                .AsParallel()
                .Count(p => IsLoop(position, direction, p));
        }
    }
}