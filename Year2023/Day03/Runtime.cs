
using Utilities;

namespace Day03;

sealed class Runtime {
    readonly char[,] map;

    public bool sumOfAdjacentParts { get; internal set; }

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllCharactersAsMap();
    }

    internal bool TryGet(int x, int y, out char symbol) {
        if (!IsValid(x, y)) {
            symbol = default;
            return false;
        }

        symbol = map[x, y];
        return true;
    }

    bool IsValid(int x, int y)
        => x >= 0 && x < map.GetLength(0)
        && y >= 0 && y < map.GetLength(1);
}