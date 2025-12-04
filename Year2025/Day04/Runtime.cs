using Utilities;

namespace Day04;

sealed partial class Runtime {
    readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
    }

    internal int accessibleRollCount => map
        .allPositionsAndCharactersWithin
        .Count(IsAccessible);

    internal int accessibleRollCountWithRemoving {
        get {
            int count = 0;
            bool hasRemoved;
            do {
                hasRemoved = false;
                foreach (var tile in map.allPositionsAndCharactersWithin) {
                    if (IsAccessible(tile)) {
                        map[tile.position] = '.';
                        count++;
                        hasRemoved = true;
                    }
                }
            } while (hasRemoved);
            return count;
        }
    }

    bool IsAccessible((Vector2Int position, char character) tile) {
        if (tile.character is not '@') {
            return false;
        }

        int count = 0;
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) {
                    continue;
                }

                if (map.TryGet(tile.position + new Vector2Int(x, y), out char test) && test is '@') {
                    count++;
                    if (count == 4) {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}