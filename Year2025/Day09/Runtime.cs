using Utilities;

namespace Day09;

sealed partial class Runtime {
    readonly List<Vector2Int> redTiles = [];

    bool IsRed(Vector2Int position) => redTiles.Contains(position);

    internal bool IsGreenOrRed(Vector2Int position) {
        if (IsRed(position)) {
            return true;
        }

        bool isInside = false;
        for (int i = 0, j = redTiles.Count - 1; i < redTiles.Count; i++) {
            var start = redTiles[j];
            var end = redTiles[i];
            if ((start.y < position.y && end.y >= position.y) || (end.y < position.y && start.y >= position.y)) {
                if (start.x <= position.x || end.x <= position.x) {
                    isInside = !isInside;
                }
            }

            j = i;
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
                for (int j = i + 1; j < redTiles.Count; j++) {
                    yield return (redTiles[i], redTiles[j]);
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
            }
        }
    }

    internal long largestRectangleArea => uniquePairs
        .Max(pair => Vector2Int.RectangleArea(pair.left, pair.right));

    internal long largestRectangleAreaOnlyGreen => uniquePairsOnlyGreen
        .Max(pair => Vector2Int.RectangleArea(pair.left, pair.right));
}