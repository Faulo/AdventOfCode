using NUnit.Framework;
using Utilities;

namespace Day10;

sealed class Runtime {
    internal char this[Vector2 position] {
        get {
            return map[position.x, position.y];
        }
    }

    readonly char[,] map;
    readonly int width;
    readonly int height;
    internal readonly Vector2 start;

    readonly HashSet<Vector2> path;
    internal int maximumDistance => path.Count / 2;
    internal int enclosedArea {
        get {
            int i = 0;
            for (int x = 0; x < width; x++) {
                var borderCount = new Dictionary<Directions, int> {
                    [Directions.Left] = 0,
                    [Directions.Right] = 0,
                };
                for (int y = 0; y < height; y++) {
                    var position = new Vector2(x, y);
                    bool isOnPath = IsOnPath(position);
                    var directions = this[position].GetDirections();
                    bool isBorder = isOnPath && (directions & (Directions.Left | Directions.Right)) != 0;

                    if (isBorder) {
                        if (directions.HasFlag(Directions.Left)) {
                            borderCount[Directions.Left]++;
                        }

                        if (directions.HasFlag(Directions.Right)) {
                            borderCount[Directions.Right]++;
                        }
                    }

                    if (isOnPath || borderCount.Values.Any(c => c % 2 == 1)) {
                        i++;
                    }
                }

                Assert.That(borderCount.Values.All(c => c % 2 == 0), Is.True, $"Failed to determine borders for x={x}");
            }

            return i - path.Count;
        }
    }

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllCharactersAsMap();
        width = map.GetLength(0);
        height = map.GetLength(1);
        start = GetStart();

        map[start.x, start.y] = GetCharacterPointingTo(start);

        var previousPoint = start;
        var point = GetNeighborsPointingTo(previousPoint).First();
        path = [previousPoint];
        while (path.Add(point)) {
            var newPoint = GetNeighborsPointingFrom(point).First(p => p != previousPoint);
            previousPoint = point;
            point = newPoint;
        }
    }

    Vector2 GetStart() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (map[x, y] == 'S') {
                    return new(x, y);
                }
            }
        }

        throw new Exception();
    }

    internal char GetCharacterPointingTo(Vector2 position) {
        var direction = Directions.None;
        foreach (var neighbor in GetNeighbors(position)) {
            foreach (var offset in this[neighbor].GetDirections().GetOffsets()) {
                if (offset == position - neighbor) {
                    direction |= new Vector2(-offset.x, -offset.y).GetDirections();
                }
            }
        }

        return direction.GetCharacter();
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
    internal bool IsOnPath(Vector2 position) {
        return path.Contains(position);
    }
}

sealed record Vector2(int x, int y) {
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
}

static class Extensions {
    internal static Directions GetDirections(this char pipe) {
        return pipe switch {
            '|' => Directions.Up | Directions.Down, // is a vertical pipe connecting north and south.
            '-' => Directions.Right | Directions.Left, // is a horizontal pipe connecting east and west.
            'L' => Directions.Up | Directions.Right, // is a 90 - degree bend connecting north and east.
            'J' => Directions.Up | Directions.Left, // is a 90 - degree bend connecting north and west.
            '7' => Directions.Down | Directions.Left, // is a 90 - degree bend connecting south and west.
            'F' => Directions.Down | Directions.Right, // is a 90-degree bend connecting south and east.
            _ => Directions.None,
        };
    }

    internal static char GetCharacter(this Directions direction) {
        return direction switch {
            Directions.Up | Directions.Down => '|', // is a vertical pipe connecting north and south.
            Directions.Right | Directions.Left => '-', // is a horizontal pipe connecting east and west.
            Directions.Up | Directions.Right => 'L', // is a 90 - degree bend connecting north and east.
            Directions.Up | Directions.Left => 'J', // is a 90 - degree bend connecting north and west.
            Directions.Down | Directions.Left => '7', // is a 90 - degree bend connecting south and west.
            Directions.Down | Directions.Right => 'F', // is a 90-degree bend connecting south and east.
            _ => '.',
        };
    }

    internal static IEnumerable<Vector2> GetOffsets(this Directions directions) {
        if (directions.HasFlag(Directions.Up)) {
            yield return new(0, -1);
        }

        if (directions.HasFlag(Directions.Right)) {
            yield return new(1, 0);
        }

        if (directions.HasFlag(Directions.Down)) {
            yield return new(0, 1);
        }

        if (directions.HasFlag(Directions.Left)) {
            yield return new(-1, 0);
        }
    }

    internal static Directions GetDirections(this Vector2 offset) {
        return (offset.x, offset.y) switch {
            (0, -1) => Directions.Up,
            (1, 0) => Directions.Right,
            (0, 1) => Directions.Down,
            (-1, 0) => Directions.Left,
            _ => Directions.None,
        };
    }
}