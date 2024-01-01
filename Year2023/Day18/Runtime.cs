using System.Globalization;
using Utilities;

namespace Day18;

sealed class Runtime {
    internal readonly long totalDigArea;

    readonly List<Vector2Int> path = [];
    readonly long width;
    readonly long height;

    internal Runtime(string file, bool useColor = false) {
        var position = Vector2Int.zero;
        path.Add(position);
        foreach (string line in new FileInput(file).ReadLines()) {
            var offset = useColor
                ? ParseColor(line)
                : ParseLine(line);

            position += offset;
            path.Add(position);
        }

        TransposeToPositive(path);

        totalDigArea = 0;
        long perimeter = 0;
        for (int i = 0, j = path.Count - 1; i < path.Count; i++) {
            perimeter += Math.Abs((path[i] - path[j]).manhattenDistance);
            totalDigArea += path[i].x64 * path[j].y64;
            totalDigArea -= path[j].x64 * path[i].y64;
            j = i;
        }

        perimeter /= 2;
        perimeter++;

        totalDigArea /= 2;

        totalDigArea = Math.Abs(totalDigArea);

        totalDigArea += perimeter;

        width = path.Max(p => p.x) + 1;
        height = path.Max(p => p.y) + 1;
    }

    internal static Vector2Int ParseLine(string line) {
        string[] cells = line.Split(' ');
        int value = int.Parse(cells[1]);
        return cells[0] switch {
            "U" => Vector2Int.up,
            "D" => Vector2Int.down,
            "L" => Vector2Int.left,
            "R" => Vector2Int.right,
            _ => Vector2Int.zero,
        } * value;
    }

    internal static Vector2Int ParseColor(string line) {
        string[] cells = line.Split(' ');
        int value = int.Parse(cells[2][2..^2], NumberStyles.HexNumber);
        int direction = int.Parse(cells[2][^2..^1]);
        return direction switch {
            0 => Vector2Int.right,
            1 => Vector2Int.down,
            2 => Vector2Int.left,
            3 => Vector2Int.up,
            _ => Vector2Int.zero,
        } * value;
    }

    internal static void TransposeToPositive(IList<Vector2Int> path) {
        long x = path.Min(p => p.x64);
        long y = path.Min(p => p.y64);

        x = x < 0
            ? Math.Abs(x)
            : 0;
        y = y < 0
            ? Math.Abs(y)
            : 0;

        var offset = new Vector2Int(x, y);

        for (int i = 0; i < path.Count; i++) {
            path[i] += offset;
        }
    }

    internal static long ScaleToSmallest(IList<Vector2Int> path) {
        long scale = Math.Min(
            path.Where(p => p.x64 > 0).Min(p => p.x64),
            path.Where(p => p.y64 > 0).Min(p => p.y64)
        );

        while (scale > 1) {
            bool isMatch = true;
            for (int i = 0; i < path.Count; i++) {
                if (path[i].x % scale != 0 || path[i].y % scale != 0) {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch) {
                for (int i = 0; i < path.Count; i++) {
                    path[i] /= scale;
                }

                return scale;
            }

            scale--;
        }

        return scale;
    }

    internal static IEnumerable<Vector2Int> MoveBetween(Vector2Int start, Vector2Int goal) {
        if (start.x == goal.x) {
            int direction = goal.y - start.y > 0
                ? 1
                : -1;
            for (long y = start.y; y != goal.y; y += direction) {
                yield return new(start.x, y);
            }
        } else {
            int direction = goal.x - start.x > 0
                ? 1
                : -1;
            for (long x = start.x; x != goal.x; x += direction) {
                yield return new(x, start.y);
            }
        }
    }
}

static class Extensions {
}