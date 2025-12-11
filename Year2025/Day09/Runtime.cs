using System.Collections.Concurrent;
using Utilities;

namespace Day09;

sealed partial class Runtime {
    readonly List<Vector2Int> redTiles = [];

    bool IsRed(Vector2Int position) => redTiles.Contains(position);

    internal bool IsOnBorder(Vector2Int position) {
        if (IsRed(position)) {
            return true;
        }

        for (int i = 0, j = redTiles.Count - 1; i < redTiles.Count; j = i, i++) {
            var start = redTiles[i];
            var end = redTiles[j];

            if (start.x == end.x) {
                if (position.x == start.x && position.y >= Math.Min(start.y, end.y) && position.y <= Math.Max(start.y, end.y)) {
                    return true;
                }
            } else {
                if (position.y == start.y && position.x >= Math.Min(start.x, end.x) && position.x <= Math.Max(start.x, end.x)) {
                    return true;
                }
            }
        }

        return false;
    }

    readonly ConcurrentDictionary<Vector2Int, bool> greenOrRedCache = [];

    internal bool IsGreenOrRed(Vector2Int position) {
        return greenOrRedCache.TryGetValue(position, out bool result)
            ? result
            : greenOrRedCache[position] = IsGreenOrRedInternal(position);
    }

    bool IsGreenOrRedInternal(Vector2Int position) {
        if (IsOnBorder(position)) {
            return true;
        }

        bool isInside = false;
        for (int i = 0, j = redTiles.Count - 1; i < redTiles.Count; j = i, i++) {
            var start = redTiles[i];
            var end = redTiles[j];
            if (start.x != end.x) {
                continue;
            }

            int minY = Math.Min(start.y, end.y);
            int maxY = Math.Max(start.y, end.y);

            if (position.y >= minY && position.y < maxY && position.x <= start.x) {
                isInside = !isInside;
            }
        }

        return isInside;
    }

    IEnumerable<(Vector2Int left, Vector2Int right)> uniquePairs {
        get {
            for (int i = 0; i < redTiles.Count; i++) {
                for (int j = i + 1; j < redTiles.Count; j++) {
                    yield return (redTiles[i], redTiles[j]);
                }
            }
        }
    }

    IEnumerable<(Vector2Int left, Vector2Int right)> uniquePairsOnlyGreen {
        get {
            for (int i = 0; i < redTiles.Count; i++) {
                var start = redTiles[i];

                for (int j = i + 1; j < redTiles.Count; j++) {
                    var end = redTiles[j];

                    bool isValid = Vector2Int
                        .RectangleBorder(start, end)
                        .All(IsGreenOrRed);

                    if (isValid) {
                        yield return (start, end);
                    }
                }
            }
        }
    }

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] coordinates = line.Split(',');
            if (int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y)) {
                var position = new Vector2Int(x, y);
                redTiles.Add(position);
                greenOrRedCache[position] = true;
            }
        }
    }

    internal long largestRectangleArea => uniquePairs
        .Max(pair => Vector2Int.RectangleArea(pair.left, pair.right));

    internal long largestRectangleAreaOnlyGreen {
        get {
            long maximum = 0;

            uniquePairs.AsParallel().ForAll(pair => {
                long size = Vector2Int.RectangleArea(pair.left, pair.right);
                if (size > maximum) {
                    bool isValid = Vector2Int
                        .RectangleBorder(pair.left, pair.right)
                        .All(IsGreenOrRed);
                    if (isValid) {
                        lock (this) {
                            if (maximum < size) {
                                maximum = size;
                            }
                        }
                    }
                }
            });

            return maximum;
        }
    }
}