using Utilities;

namespace Day04;

sealed partial class Runtime {

    static readonly Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up + Vector2Int.right,
        Vector2Int.down + Vector2Int.right,
        Vector2Int.down + Vector2Int.left,
        Vector2Int.up + Vector2Int.left,
    };

    readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
    }

    const string TERM = "XMAS";

    int CountTerms(Vector2Int position) {
        return directions.Count(direction => IsTerm(position, direction));
    }

    bool IsTerm(Vector2Int position, Vector2Int direction) {
        for (int i = 0; i < TERM.Length; i++) {
            var p = position + (direction * i);
            if (!map.IsInBounds(p)) {
                return false;
            }

            if (map[p] != TERM[i]) {
                return false;
            }
        }

        return true;
    }

    char[] cross = new char[4];

    bool IsCross(Vector2Int position) {
        if (map[position] != 'A') {
            return false;
        }

        if (!map.TryGet(position + Vector2Int.up + Vector2Int.right, out cross[0])) {
            return false;
        }

        if (!map.TryGet(position + Vector2Int.down + Vector2Int.right, out cross[1])) {
            return false;
        }

        if (!map.TryGet(position + Vector2Int.down + Vector2Int.left, out cross[2])) {
            return false;
        }

        if (!map.TryGet(position + Vector2Int.up + Vector2Int.left, out cross[3])) {
            return false;
        }

        if (cross[0] == cross[2]) {
            return false;
        }

        if (cross.Count(c => c == 'M') != 2) {
            return false;
        }

        if (cross.Count(c => c == 'S') != 2) {
            return false;
        }

        return true;
    }

    internal int straightOccurences {
        get {
            return map
                .allPositionsWithin
                .Sum(CountTerms);
        }
    }

    internal int crossOccurences {
        get {
            return map
                .allPositionsWithin
                .Count(IsCross);
        }
    }
}