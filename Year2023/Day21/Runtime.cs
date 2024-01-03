using Utilities;

namespace Day21;

sealed class Runtime {
    readonly CharacterMap map;
    internal readonly Vector2Int start;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
        start = map
            .allPositionsAndCharactersWithin
            .First(spot => spot.character == 'S')
            .position;
        map[start] = '.';
    }

    internal long GetNumberOfDestinations(int steps) {
        var positions = new HashSet<Vector2Int>() { start };
        for (int i = 0; i < steps; i++) {
            var newPositions = new HashSet<Vector2Int>();
            foreach (var position in positions) {
                foreach (var neighbor in position.neighbors) {
                    if (map.IsInBounds(neighbor) && map[neighbor].IsFree()) {
                        newPositions.Add(neighbor);
                    }
                }
            }

            positions = newPositions;
        }

        return positions.Count;
    }
}

static class Extensions {
    internal static bool IsFree(this char tile) => tile == '.';
}