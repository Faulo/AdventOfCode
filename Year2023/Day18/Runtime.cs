﻿using System.Globalization;
using System.Text;
using Utilities;

namespace Day18;

sealed class Runtime {
    internal long totalDigArea => pathLength + insideArea;

    long FloodFill(Vector2Int position, char character) {
        long sum = 0;

        var queue = new HashSet<Vector2Int> {
            position
        };

        do {
            position = queue.First();
            queue.Remove(position);

            if (map[position] == '.') {
                map[position] = character;
                sum++;
                foreach (var offset in offsets) {
                    var p = position + offset;
                    if (map.IsInBounds(p) && map[p] == '.') {
                        queue.Add(p);
                    }
                }
            }
        } while (queue.Count > 0);

        return sum;
    }
    static readonly Vector2Int[] offsets = [
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
    ];

    readonly List<Vector2Int> path = [];
    readonly int width;
    readonly int height;
    readonly CharacterMap map;
    internal readonly Vector2Int firstPositionInside;

    Vector2Int GetFirstPositionInside() {
        for (int y = 1; y < height; y++) {
            for (int x = 1; x < width; x++) {
                var p = new Vector2Int(x, y);
                if (!IsOnPath(p) && IsOnPath(new(x - 1, y)) && IsOnPath(new(x, y - 1))) {
                    return p;
                }
            }
        }

        throw new Exception();
    }

    internal readonly long pathLength;
    internal readonly long insideArea;

    internal bool IsOnPath(Vector2Int position) {
        return map[position] == '#';
    }

    internal Runtime(string file, bool useColor = false) {
        var position = Vector2Int.zero;
        path.Add(position);
        foreach (string line in new FileInput(file).ReadLines()) {
            position += useColor
                ? ParseColor(line)
                : ParseLine(line);
            path.Add(position);
        }

        TransposeToPositive(path);

        width = path.Max(p => p.x) + 1;
        height = path.Max(p => p.y) + 1;

        char[,] map = new char[width, height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                map[x, y] = '.';
            }
        }

        pathLength = 0;
        for (int i = 1; i < path.Count; i++) {
            var previous = path[i - 1];
            var current = path[i];

            foreach (var p in MoveBetween(previous, current)) {
                map[p.x, p.y] = '#';
                pathLength++;
            }
        }

        this.map = new(map);

        firstPositionInside = GetFirstPositionInside();
        insideArea = FloodFill(firstPositionInside, 'o');
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
        int x = path.Min(p => p.x);
        int y = path.Min(p => p.y);

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

    internal static int ScaleToSmallest(IList<Vector2Int> path) {
        int scale = Math.Min(
            path.Where(p => p.x > 0).Min(p => p.x),
            path.Where(p => p.y > 0).Min(p => p.y)
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
            for (int y = start.y; y != goal.y; y += direction) {
                yield return new(start.x, y);
            }
        } else {
            int direction = goal.x - start.x > 0
                ? 1
                : -1;
            for (int x = start.x; x != goal.x; x += direction) {
                yield return new(x, start.y);
            }
        }
    }

    public override string ToString() {
        var builder = new StringBuilder();
        for (int y = 0; y < height; y++) {
            if (y > 0) {
                builder.AppendLine();
            }

            for (int x = 0; x < width; x++) {
                builder.Append(map[x, y]);
            }
        }

        return builder.ToString();
    }
}

static class Extensions {
}