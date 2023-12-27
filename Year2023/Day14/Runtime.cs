using Utilities;

namespace Day14;

sealed class Runtime {
    internal int northLoad {
        get {
            int sum = 0;

            for (int y = 0; y < map.height; y++) {
                int weight = map.height - y;
                for (int x = 0; x < map.width; x++) {
                    if (map[x, y].IsRound()) {
                        sum += weight;
                    }
                }
            }

            return sum;
        }
    }

    readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
    }

    internal Runtime TiltNorth() {

        bool hasMoved;
        do {
            hasMoved = false;
            for (int y = 1; y < map.height; y++) {
                for (int x = 0; x < map.width; x++) {
                    if (map[x, y - 1].IsFree() && map[x, y].IsRound()) {
                        (map[x, y - 1], map[x, y]) = (map[x, y], map[x, y - 1]);
                        hasMoved = true;
                    }
                }
            }
        } while (hasMoved);

        return this;
    }
}

static class Extensions {
    internal static bool IsRound(this char character) {
        return character is 'O';
    }
    internal static bool IsSquare(this char character) {
        return character is '#';
    }
    internal static bool IsFree(this char character) {
        return character is '.';
    }
}