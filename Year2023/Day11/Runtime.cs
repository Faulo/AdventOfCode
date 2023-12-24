using Utilities;

namespace Day11;

sealed class Runtime {
    readonly CharacterMap map;
    internal int width => map.width;
    internal int height => map.height;
    internal readonly HashSet<Vector2Int> galaxies;

    internal int sumOfShortestPaths => 0;

    internal Runtime(string file) {
        map = new FileInput(file)
            .ReadAllAsCharacterMap()
            .Expand();
        galaxies = [];
    }

    bool IsInBounds(Vector2Int position) {
        return position.x >= 0 && position.x < width
            && position.y >= 0 && position.y < height;
    }
}

static class Extensions {
    internal static bool IsGalaxy(this char pipe) {
        return pipe == '#';
    }
    internal static CharacterMap Expand(this CharacterMap map) {
        return map;
    }
}