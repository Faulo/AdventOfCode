using Utilities;

namespace Day21;

sealed class Runtime {
    readonly CharacterMap map;
    internal readonly Vector2Int start;
    internal int freeSpots;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();

        start = map
            .allPositionsAndCharactersWithin
            .First(spot => spot.character.IsStart())
            .position;

        map[start] = '.';

        freeSpots = map
            .allPositionsAndCharactersWithin
            .Count(spot => spot.character.IsFree());
    }

    internal IDictionary<Vector2Int, int> GetShortestPathToCenter() {
        var shortestPathToCenter = new Dictionary<Vector2Int, int>() {
            [start] = 0,
        };
        var currentPositions = new HashSet<Vector2Int>(shortestPathToCenter.Keys);
        int i = 1;
        while (currentPositions.Count > 0) {
            var newPositions = new HashSet<Vector2Int>();
            foreach (var position in currentPositions) {
                foreach (var neighbor in position.neighbors) {
                    if (map.IsInBounds(neighbor) && map[neighbor].IsFree() && shortestPathToCenter.TryAdd(neighbor, i)) {
                        newPositions.Add(neighbor);
                    }
                }
            }

            currentPositions = newPositions;
            i++;
        }

        return shortestPathToCenter;
    }

    internal long GetNumberOfDestinations(long steps, bool wrap = false) {
        var visited = GetShortestPathToCenter();

        int extends = map.width / 2;

        if (!wrap) {
            int parity = (int)(steps % 2);
            return visited
                .Where(path => path.Key.manhattenDistance % 2 == parity)
                .Where(path => path.Value <= steps)
                .Count();
        }

        // credit to https://github.com/villuna/aoc23/wiki/A-Geometric-solution-to-advent-of-code-2023,-day-21

        var corners = visited
            .Where(path => path.Value > extends);

        int evenCornerCount = corners
            .Count(path => path.Key.manhattenDistance % 2 == 0);
        int oddCornerCount = corners
            .Count(path => path.Key.manhattenDistance % 2 == 1);

        int evenFullCount = visited
            .Count(path => path.Key.manhattenDistance % 2 == 0);
        int oddFullCount = visited
            .Count(path => path.Key.manhattenDistance % 2 == 1);

        long repeatCount = (steps - extends) / map.width;

        return ((repeatCount + 1) * (repeatCount + 1) * oddFullCount)
            + (repeatCount * repeatCount * evenFullCount)
            - ((repeatCount + 1) * oddCornerCount)
            + (repeatCount * evenCornerCount);
    }
}

static class Extensions {
    internal static bool IsStart(this char tile) => tile == 'S';
    internal static bool IsFree(this char tile) => tile == '.';
}