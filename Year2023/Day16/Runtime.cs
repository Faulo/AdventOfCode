using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Utilities;

namespace Day16;

sealed class Runtime(string file) {
    CharacterMap map = new FileInput(file).ReadAllAsCharacterMap();

    internal int numberOfEnergizedTiles => CountEnergizedTiels(new(0, 0), Directions.Right);

    int CountEnergizedTiels(Vector2Int position, Directions direction) {
        var energized = new Directions[map.width, map.height];

        Move(energized, position, direction);

        return energized
            .AsEnumerable()
            .Count(d => d != Directions.None);
    }

    internal int maximumNumberOfEnergizedTiles {
        get {
            var counts = new ConcurrentStack<int>();

            Parallel.ForEach(startingPositions, start => counts.Push(CountEnergizedTiels(start.position, start.direction)));

            return counts.Max();
        }
    }

    IEnumerable<(Vector2Int position, Directions direction)> startingPositions {
        get {
            for (int x = 0; x < map.width; x++) {
                yield return (new(x, 0), Directions.Down);
                yield return (new(x, map.height - 1), Directions.Up);
            }

            for (int y = 0; y < map.height; y++) {
                yield return (new(0, y), Directions.Right);
                yield return (new(map.width - 1, y), Directions.Left);
            }
        }
    }

    static readonly (Directions, Vector2Int)[] directions = [
        (Directions.Up,  Vector2Int.up),
        (Directions.Down,  Vector2Int.down),
        (Directions.Left,  Vector2Int.left),
        (Directions.Right,  Vector2Int.right),
    ];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool ShouldMove(in Directions[,] energized, in Vector2Int position, in Directions direction) {
        return map.IsInBounds(position) && !energized[position.x, position.y].HasFlag(direction);
    }

    void Move(Directions[,] energized, Vector2Int position, Directions direction) {
        if (!ShouldMove(energized, position, direction)) {
            return;
        }

        energized[position.x, position.y] |= direction;

        var next = map[position].GetDirections(direction);

        foreach (var (nextDirection, offset) in directions) {
            if (next.HasFlag(nextDirection)) {
                var nextPosition = position + offset;
                for (; ShouldMove(energized, nextPosition, nextDirection) && map[nextPosition].IsStraight(nextDirection); nextPosition += offset) {
                    energized[nextPosition.x, nextPosition.y] |= nextDirection;
                }

                Move(energized, nextPosition, nextDirection);
            }
        }
    }
}

static class Extensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsStraight(this char tile, Directions direction) {
        return tile switch {
            '.' => true,
            '-' => direction is Directions.Left or Directions.Right,
            '|' => direction is Directions.Up or Directions.Down,
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Directions GetDirections(this char tile, Directions source) {
        return tile switch {
            '.' => source,
            '-' when source is Directions.Up or Directions.Down => Directions.Left | Directions.Right,
            '-' => source,
            '|' when source is Directions.Left or Directions.Right => Directions.Up | Directions.Down,
            '|' => source,
            '\\' => source switch {
                Directions.Up => Directions.Left,
                Directions.Down => Directions.Right,
                Directions.Left => Directions.Up,
                Directions.Right => Directions.Down,
                _ => Directions.None,
            },
            '/' => source switch {
                Directions.Up => Directions.Right,
                Directions.Down => Directions.Left,
                Directions.Left => Directions.Down,
                Directions.Right => Directions.Up,
                _ => Directions.None,
            },
            _ => Directions.None,
        };
    }
}