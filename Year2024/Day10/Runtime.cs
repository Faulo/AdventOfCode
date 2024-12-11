using Utilities;

namespace Day10;

sealed partial class Runtime(string file) {

    internal CharacterMap map = new FileInput(file)
        .ReadAllAsCharacterMap();

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

    internal long trailheadCount {
        get {
            return map
                .allPositionsAndCharactersWithin
                .Where(tile => tile.character == '0')
                .Sum(tile => TrailheadCountForPosition(tile.position));
        }
    }
}