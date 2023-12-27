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
                int next = y - 1;
                for (int x = 0; x < map.width; x++) {
                    if (map[x, next].IsFree() && map[x, y].IsRound()) {
                        (map[x, next], map[x, y]) = (map[x, y], map[x, next]);
                        hasMoved = true;
                    }
                }
            }
        } while (hasMoved);

        return this;
    }

    internal Runtime TiltSouth() {

        bool hasMoved;
        do {
            hasMoved = false;
            for (int y = 0; y < map.height - 1; y++) {
                int next = y + 1;
                for (int x = 0; x < map.width; x++) {
                    if (map[x, next].IsFree() && map[x, y].IsRound()) {
                        (map[x, next], map[x, y]) = (map[x, y], map[x, next]);
                        hasMoved = true;
                    }
                }
            }
        } while (hasMoved);

        return this;
    }

    internal Runtime TiltWest() {
        bool hasMoved;
        do {
            hasMoved = false;
            for (int x = 1; x < map.width; x++) {
                int next = x - 1;
                for (int y = 0; y < map.height; y++) {
                    if (map[next, y].IsFree() && map[x, y].IsRound()) {
                        (map[next, y], map[x, y]) = (map[x, y], map[next, y]);
                        hasMoved = true;
                    }
                }
            }
        } while (hasMoved);

        return this;
    }

    internal Runtime TiltEast() {
        bool hasMoved;
        do {
            hasMoved = false;
            for (int x = 0; x < map.width - 1; x++) {
                int next = x + 1;
                for (int y = 0; y < map.height; y++) {
                    if (map[next, y].IsFree() && map[x, y].IsRound()) {
                        (map[next, y], map[x, y]) = (map[x, y], map[next, y]);
                        hasMoved = true;
                    }
                }
            }
        } while (hasMoved);

        return this;
    }

    internal Runtime CycleTilt(int cycles) {
        Dictionary<string, string> cycleCache = [];

        string key = map.Serialize();

        for (int i = 0; i < cycles; i++) {
            if (!cycleCache.TryGetValue(key, out string? newKey)) {
                map.Deserialize(key);
                TiltNorth();
                TiltWest();
                TiltSouth();
                TiltEast();
                newKey = map.Serialize();
                cycleCache[key] = newKey;
            }

            key = newKey;
        }

        map.Deserialize(key);

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