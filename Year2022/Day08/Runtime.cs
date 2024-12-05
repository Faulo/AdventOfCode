using Utilities;

namespace Day08;

class Runtime {
    readonly CharacterMap map;

    internal Runtime(string logFile) {
        map = new FileInput(logFile).ReadAllAsCharacterMap();
    }

    bool IsVisible(Vector2Int position) {
        char height = map[position];
        foreach (var direction in CharacterMap.offsets) {
            for (int i = 1, j = Math.Max(map.width, map.height); i < j; i++) {
                var p = position + (direction * i);
                if (!map.IsInBounds(p)) {
                    return true;
                }

                if (map[p] >= height) {
                    break;
                }
            }
        }

        return false;
    }

    internal int visibleTreeCount => map
        .allPositionsWithin
        .Count(IsVisible);

    int ScenicScore(Vector2Int position) {
        char height = map[position];
        int score = 1;

        foreach (var direction in CharacterMap.offsets) {
            for (int i = 1, j = Math.Max(map.width, map.height); i < j; i++) {
                var p = position + (direction * i);
                if (!map.IsInBounds(p)) {
                    score *= i - 1;
                    break;
                }

                if (map[p] >= height) {
                    score *= i;
                    break;
                }
            }
        }

        return score;
    }

    internal int highestScenicScore => map
        .allPositionsWithin
        .Max(ScenicScore);
}
