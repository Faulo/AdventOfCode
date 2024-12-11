using Utilities;

namespace Day10;

sealed partial class Runtime {

    internal readonly CharacterMap map;

    internal readonly HashSet<Vector2Int> startPositions;

    public Runtime(string file) {
        map = new FileInput(file)
            .ReadAllAsCharacterMap();

        startPositions = map
            .allPositionsAndCharactersWithin
            .Where(tile => tile.character == '0')
            .Select(tile => tile.position)
            .ToHashSet();
    }

    int TrailheadCountForPosition(Vector2Int start) {
        var currentQueue = new HashSet<Vector2Int> {
            start
        };
        var nextQueue = new HashSet<Vector2Int>();
        char height = '0';

        do {
            height++;

            foreach (var position in currentQueue) {
                foreach (var (neighborPosition, neighborCharacter) in map.GetNeighbors(position)) {
                    if (neighborCharacter == height) {
                        nextQueue.Add(neighborPosition);
                    }
                }
            }

            (currentQueue, nextQueue) = (nextQueue, currentQueue);
            nextQueue.Clear();
        } while (height != '9' && currentQueue.Count > 0);

        return currentQueue.Count;
    }

    internal int RatingCountForPosition(Vector2Int start) {
        var currentQueue = new Dictionary<Vector2Int, int> {
            [start] = 1,
        };
        var nextQueue = new Dictionary<Vector2Int, int>();
        char height = '0';

        do {
            height++;

            foreach (var (position, count) in currentQueue) {

                foreach (var (neighborPosition, neighborCharacter) in map.GetNeighbors(position)) {
                    if (neighborCharacter == height) {
                        nextQueue[neighborPosition] = nextQueue.TryGetValue(neighborPosition, out int c)
                            ? c + count
                            : count;
                    }
                }
            }

            (currentQueue, nextQueue) = (nextQueue, currentQueue);
            nextQueue.Clear();
        } while (height != '9' && currentQueue.Count > 0);

        return currentQueue.Values.Sum();
    }

    internal long trailheadCount {
        get {
            return startPositions
                .Sum(TrailheadCountForPosition);
        }
    }

    internal long ratingCount {
        get {
            return startPositions
                .Sum(RatingCountForPosition);
        }
    }
}