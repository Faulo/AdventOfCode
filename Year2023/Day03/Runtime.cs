using Utilities;

namespace Day03;

sealed class Runtime {
    readonly char[,] map;
    internal readonly int width;
    internal readonly int height;

    internal int sumOfAdjacentParts {
        get {
            int sum = 0;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (TryGetAdjacentPart(x, y, out int part)) {
                        sum += part;
                    }
                }
            }

            return sum;
        }
    }
    internal int sumOfAdjacentGears {
        get {
            int sum = 0;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (TryGetAdjacentGear(x, y, out int part)) {
                        sum += part;
                    }
                }
            }

            return sum;
        }
    }

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllCharactersAsMap();
        width = map.GetLength(0);
        height = map.GetLength(1);
    }

    internal bool TryGetSymbol(int x, int y, out char symbol) {
        if (!IsValid(x, y)) {
            symbol = default;
            return false;
        }

        symbol = map[x, y];
        return true;
    }

    internal bool TryGetAdjacentPart(int x, int y, out int number, bool walkLeft = false) {
        if (TryGetSymbol(x, y, out char symbol) && !char.IsDigit(symbol)) {
            number = default;
            return false;
        }

        if (TryGetSymbol(x - 1, y, out symbol) && char.IsDigit(symbol)) {
            if (walkLeft) {
                return TryGetAdjacentPart(x - 1, y, out number, walkLeft);
            }

            number = default;
            return false;
        }

        string result = "";
        bool isAdjacent = false;
        for (; x < width; x++) {
            if (!TryGetSymbol(x, y, out symbol)) {
                break;
            }

            if (char.IsDigit(symbol)) {
                result += symbol;

                if (!isAdjacent) {
                    isAdjacent = IsAdjacentToPart(x, y);
                }
            } else {
                break;
            }
        }

        number = result == ""
            ? default
            : int.Parse(result);

        return isAdjacent;
    }

    internal bool TryGetAdjacentGear(int x, int y, out int gear) {
        if (!TryGetSymbol(x, y, out char symbol) || symbol != '*') {
            gear = default;
            return false;
        }

        HashSet<int> parts = [];
        foreach (var offset in neighbors) {
            if (TryGetAdjacentPart(x + offset.x, y + offset.y, out int part, true)) {
                parts.Add(part);
            }
        }

        if (parts.Count != 2) {
            gear = default;
            return false;
        }

        gear = parts.First() * parts.Last();
        return true;
    }

    bool IsValid(int x, int y)
        => x >= 0 && x < width
        && y >= 0 && y < height;

    static readonly IEnumerable<(int x, int y)> neighbors = [
        (+0, +1),
        (+1, +0),
        (+1, +1),
        (+0, -1),
        (-1, +0),
        (-1, -1),
        (+1, -1),
        (-1, +1),
    ];

    bool IsAdjacentToPart(int x, int y)
        => neighbors.Any(offset => IsPart(x + offset.x, y + offset.y));

    bool IsPart(int x, int y)
        => TryGetSymbol(x, y, out char symbol)
        && symbol != '.'
        && !char.IsDigit(symbol);
}