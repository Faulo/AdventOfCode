using Utilities;

namespace Day10;

sealed class Runtime {
    char this[Vector2 position] {
        get {
            return map[position.x, position.y];
        }
    }

    readonly char[,] map;
    int width => map.GetLength(0);
    int height => map.GetLength(1);

    internal int maximumDistance {
        get {
            var paths = GetNeighborsPointingTo(start)
                .ToList();
            var first = paths[0];
            var second = paths[1];

            int i = 1;

            var previousFirst = start;
            var previousSecond = start;
            while (first != second) {
                i++;

                var newFirst = GetNeighborsPointingFrom(first).First(p => p != previousFirst);
                var newSecond = GetNeighborsPointingFrom(second).First(p => p != previousSecond);

                previousFirst = first;
                previousSecond = second;

                first = newFirst;
                second = newSecond;
            }

            return i;
        }
    }

    internal Vector2 start {
        get {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (map[x, y] == 'S') {
                        return new(x, y);
                    }
                }
            }

            throw new Exception();
        }
    }

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllCharactersAsMap();
    }

    internal IEnumerable<Vector2> GetNeighborsPointingTo(Vector2 position) {
        foreach (var neighbor in GetNeighbors(position)) {
            if (this[neighbor].GetDirections().GetOffsets().Any(offset => offset == position - neighbor)) {
                yield return neighbor;
            }
        }
    }

    internal IEnumerable<Vector2> GetNeighborsPointingFrom(Vector2 position) {
        return this[position]
            .GetDirections()
            .GetOffsets()
            .Select(offset => position + offset);
    }

    IEnumerable<Vector2> GetNeighbors(Vector2 position) {
        for (int x = position.x - 1; x <= position.x + 1; x++) {
            for (int y = position.y - 1; y <= position.y + 1; y++) {
                var neighbor = new Vector2(x, y);
                if (IsInBounds(neighbor) && neighbor != position) {
                    yield return neighbor;
                }
            }
        }
    }
    bool IsInBounds(Vector2 position) {
        return position.x >= 0 && position.x < width
            && position.y >= 0 && position.y < height;
    }
}

enum Directions {
    None = 0,
    North = 1 << 0,
    East = 1 << 1,
    South = 1 << 2,
    West = 1 << 3,
}

sealed record Vector2(int x, int y) {
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
}

static class Extensions {
    internal static Directions GetDirections(this char pipe) {
        return pipe switch {
            '|' => Directions.North | Directions.South, // is a vertical pipe connecting north and south.
            '-' => Directions.East | Directions.West, // is a horizontal pipe connecting east and west.
            'L' => Directions.North | Directions.East, // is a 90 - degree bend connecting north and east.
            'J' => Directions.North | Directions.West, // is a 90 - degree bend connecting north and west.
            '7' => Directions.South | Directions.West, // is a 90 - degree bend connecting south and west.
            'F' => Directions.South | Directions.East, // is a 90-degree bend connecting south and east.
            _ => Directions.None,
        };
    }

    internal static IEnumerable<Vector2> GetOffsets(this Directions directions) {
        if (directions.HasFlag(Directions.North)) {
            yield return new(0, -1);
        }

        if (directions.HasFlag(Directions.East)) {
            yield return new(1, 0);
        }

        if (directions.HasFlag(Directions.South)) {
            yield return new(0, 1);
        }

        if (directions.HasFlag(Directions.West)) {
            yield return new(-1, 0);
        }
    }
}