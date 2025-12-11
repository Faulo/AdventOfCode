using Utilities;

namespace Day09;

sealed partial class Runtime {
    readonly List<Vector2Int> redTiles = [];

    IEnumerable<(Vector2Int start, Vector2Int end)> redBorder {
        get {
            for (int i = 0, j = redTiles.Count - 1; i < redTiles.Count; j = i, i++) {
                yield return (redTiles[i], redTiles[j]);
            }
        }
    }

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

    readonly Dictionary<Vector2Int, bool> greenOrRedCache = [];

    internal bool IsGreenOrRed(Vector2Int position) {
        return greenOrRedCache.TryGetValue(position, out bool result)
            ? result
            : greenOrRedCache[position] = IsGreenOrRedInternal(position);
    }

    bool IsGreenOrRedInternal(Vector2Int position) {
        bool isInside = false;
        for (int i = 0, j = redTiles.Count - 1; i < redTiles.Count; j = i, i++) {
            var start = redTiles[i];
            var end = redTiles[j];

            if (start.x == end.x) {
                if (position.x == start.x && position.y >= Math.Min(start.y, end.y) && position.y <= Math.Max(start.y, end.y)) {
                    return true;
                }

                int minY = Math.Min(start.y, end.y);
                int maxY = Math.Max(start.y, end.y);

                if (position.y >= minY && position.y < maxY && position.x <= start.x) {
                    isInside = !isInside;
                }
            } else {
                if (position.y == start.y && position.x >= Math.Min(start.x, end.x) && position.x <= Math.Max(start.x, end.x)) {
                    return true;
                }
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

            foreach (var (left, right) in uniquePairs) {
                long size = Vector2Int.RectangleArea(left, right);
                if (size > maximum) {
                    if (IsGreenOrRed(new(left.x, right.y)) && IsGreenOrRed(new(right.x, left.y))) {
                        int minX = Math.Min(left.x, right.x);
                        int maxX = Math.Max(left.x, right.x);
                        int minY = Math.Min(left.y, right.y);
                        int maxY = Math.Max(left.y, right.y);

                        bool isValid = redBorder
                            .All(border => {
                                int borderMinX = Math.Min(border.start.x, border.end.x);
                                int borderMaxX = Math.Max(border.start.x, border.end.x);

                                int borderMinY = Math.Min(border.start.y, border.end.y);
                                int borderMaxY = Math.Max(border.start.y, border.end.y);

                                if (borderMinX == borderMaxX) {
                                    // vertical
                                    if (minX < borderMinX && borderMinX < maxX) {
                                        if (borderMinY < minY) {
                                            return borderMaxY <= minY;
                                        }

                                        if (borderMaxY > maxY) {
                                            return borderMinY >= maxY;
                                        }

                                        return true;
                                    } else {
                                        return true;
                                    }
                                } else {
                                    // horizontal
                                    if (minY < borderMinY && borderMinY < maxY) {
                                        if (borderMinX < minX) {
                                            return borderMaxX <= minX;
                                        }

                                        if (borderMaxX > maxX) {
                                            return borderMinX >= maxX;
                                        }

                                        return true;
                                    } else {
                                        return true;
                                    }
                                }
                            });

                        if (isValid) {
                            maximum = size;
                        }
                    }
                }
            };

            return maximum;
        }
    }

    internal long largestRectangleAreaOnlyGreenSingleThreaded {
        get {
            long maximum = 0;

            foreach (var (left, right) in uniquePairs) {
                long size = Vector2Int.RectangleArea(left, right);
                if (size > maximum) {
                    bool isValid = Vector2Int
                        .RectangleBorder(left, right)
                        .All(IsGreenOrRed);
                    if (isValid) {
                        maximum = size;
                    }
                }
            };

            return maximum;
        }
    }

    internal long largestRectangleAreaOnlyGreenLINQ => uniquePairs
        .AsParallel()
        .Select(pair => (pair.left, pair.right, Vector2Int.RectangleArea(pair.left, pair.right)))
        .Where(pair => Vector2Int.RectangleBorder(pair.left, pair.right).All(IsGreenOrRed))
        .Max(pair => pair.Item3);

    internal long largestRectangleAreaOnlyGreenMultiThreaded {
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