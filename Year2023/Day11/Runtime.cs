using Utilities;

namespace Day11;

sealed class Runtime {
    readonly CharacterMap map;
    readonly int expansion;
    internal int expandedWidth => map.width + doubleColumns.Count;
    internal int expandedHeight => map.height + doubleRows.Count;
    internal readonly List<Vector2Int> galaxies;
    IEnumerable<(Vector2Int g1, Vector2Int g2)> galaxyPairs {
        get {
            for (int i = 0; i < galaxies.Count; i++) {
                for (int j = i + 1; j < galaxies.Count; j++) {
                    yield return (galaxies[i], galaxies[j]);
                }
            }
        }
    }
    internal readonly HashSet<int> doubleRows;
    internal readonly HashSet<int> doubleColumns;

    internal long sumOfShortestPaths => galaxyPairs
        .Sum(pair => CalculateDistance(pair.g1, pair.g2));

    internal Runtime(string file, int expansion = 2) {
        this.expansion = expansion;

        map = new FileInput(file)
            .ReadAllAsCharacterMap();

        doubleRows = Enumerable.Range(0, map.width)
            .Where(x => !map.HasGalaxyInColumn(x))
            .ToHashSet();

        doubleColumns = Enumerable.Range(0, map.height)
            .Where(y => !map.HasGalaxyInRow(y))
            .ToHashSet();

        galaxies = map
            .allPositionsAndCharactersWithin
            .Where(item => item.character.IsGalaxy())
            .Select(item => item.position)
            .ToList();
    }

    internal long CalculateDistance(Vector2Int a, Vector2Int b) {
        var start = new Vector2Int(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        var stop = new Vector2Int(Math.Max(a.x, b.x), Math.Max(a.y, b.y));

        long distance = 0;

        for (int x = start.x; x < stop.x; x++) {
            if (doubleRows.Contains(x)) {
                distance += expansion;
            } else {
                distance++;
            }
        }

        for (int y = start.y; y < stop.y; y++) {
            if (doubleColumns.Contains(y)) {
                distance += expansion;
            } else {
                distance++;
            }
        }

        return distance;
    }
}

static class Extensions {
    internal static bool IsGalaxy(this char character) {
        return character == '#';
    }
    internal static bool HasGalaxyInRow(this CharacterMap map, int y) {
        for (int x = 0; x < map.width; x++) {
            if (map[(x, y)].IsGalaxy()) {
                return true;
            }
        }

        return false;
    }
    internal static bool HasGalaxyInColumn(this CharacterMap map, int x) {
        for (int y = 0; y < map.height; y++) {
            if (map[(x, y)].IsGalaxy()) {
                return true;
            }
        }

        return false;
    }
}