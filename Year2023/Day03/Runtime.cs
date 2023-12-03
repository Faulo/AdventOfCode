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

    internal bool TryGetAdjacentPart(int x, int y, out int number) {
        if (TryGetSymbol(x - 1, y, out char symbol) && char.IsDigit(symbol)) {
            number = default;
            return false;
        }

        string result = "";
        bool isAdjacent = false;
        for (; x < width; x++) {
            if (!isAdjacent) {
                isAdjacent = IsAdjacent(x, y);
            }

            if (!TryGetSymbol(x, y, out symbol)) {
                break;
            }

            if (char.IsDigit(symbol)) {
                result += symbol;
            } else {
                break;
            }
        }

        number = result == ""
            ? default
            : int.Parse(result);

        return isAdjacent;
    }

    bool IsValid(int x, int y)
        => x >= 0 && x < width
        && y >= 0 && y < height;

    bool IsAdjacent(int x, int y)
        => IsPart(x + 0, y + 1)
        || IsPart(x + 1, y + 0)
        || IsPart(x + 1, y + 1)
        || IsPart(x + 0, y - 1)
        || IsPart(x - 1, y + 0)
        || IsPart(x - 1, y - 1)
        || IsPart(x + 1, y - 1)
        || IsPart(x - 1, y + 1);

    bool IsPart(int x, int y)
        => TryGetSymbol(x, y, out char symbol)
        && symbol != '.'
        && !char.IsDigit(symbol);
}