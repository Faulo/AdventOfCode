using Utilities;

namespace Day16;

sealed class Runtime(string file) {
    CharacterMap map = new FileInput(file).ReadAllAsCharacterMap();

    internal int numberOfEnergizedTiles {
        get {
            var energized = new Directions[map.width, map.height];

            Move(energized, 0, 0, Directions.Right);

            return energized
                .AsEnumerable()
                .Count(d => d != Directions.None);
        }
    }

    void Move(Directions[,] energized, int x, int y, Directions direction) {
        if (energized[x, y].HasFlag(direction)) {
            return;
        }

        energized[x, y] |= direction;
    }
}
[Flags]
enum Directions {
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
}

static class Extensions {
    internal static IEnumerable<T> AsEnumerable<T>(this T[,] map) {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                yield return map[x, y];
            }
        }
    }
}