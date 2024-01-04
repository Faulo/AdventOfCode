using Utilities;

namespace Day22;

sealed class Runtime {
    internal int numberOfSuperfluousBricks => bricks.Count(IsSuperfluous);

    internal readonly List<Brick> bricks = [];

    internal readonly int width;
    internal readonly int depth;
    internal readonly int height;

    readonly Brick?[,,] map;
    readonly Brick floor;

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            bricks.Add(Brick.Parse(line));
        }

        width = bricks.Max(b => b.to.x) + 1;
        depth = bricks.Max(b => b.to.y) + 1;
        height = bricks.Max(b => b.to.z) + 1;

        map = new Brick[width, depth, height];

        floor = new() {
            from = new(0, 0, 0),
            to = new(width - 1, depth - 1, 0),
        };
        PlaceBrick(floor);

        foreach (var brick in bricks) {
            PlaceBrick(brick);
        }

        bool hasMoved;
        do {
            hasMoved = false;
            foreach (var brick in bricks) {
                int validOffset = 0;
                for (int z = brick.from.z - 1; z > 0; z--) {
                    if (IsFree(brick.positions.Select(p => p.WithZ(z)))) {
                        validOffset = z - brick.from.z;
                    } else {
                        break;
                    }
                }

                if (validOffset != 0) {
                    MoveBrick(brick, new(0, 0, validOffset));
                }
            }
        } while (hasMoved);
    }

    internal bool IsSuperfluous(Brick brick) {
        return GetNeighbors(brick, Vector3Int.up)
            .All(neighbor => GetNeighbors(neighbor, Vector3Int.down).Count > 1);
    }

    internal ISet<Brick> GetNeighbors(Brick brick, Vector3Int offset) {
        var result = new HashSet<Brick>();
        foreach (var position in brick.positions) {
            if (map[position.x + offset.x, position.y + offset.y, position.z + offset.z] is Brick neighbor && neighbor != brick) {
                result.Add(neighbor);
            }
        }

        return result;
    }

    void PlaceBrick(Brick brick) {
        foreach (var position in brick.positions) {
            map[position.x, position.y, position.z] = brick;
        }
    }

    void MoveBrick(Brick brick, Vector3Int offset) {
        foreach (var position in brick.positions) {
            map[position.x, position.y, position.z] = null;
        }

        brick.from += offset;
        brick.to += offset;
        PlaceBrick(brick);
    }

    bool IsFree(IEnumerable<Vector3Int> positions) {
        foreach (var position in positions) {
            if (map[position.x, position.y, position.z] is not null) {
                return false;
            }
        }

        return true;
    }
}

class Brick {
    internal Vector3Int from;
    internal Vector3Int to;

    internal IEnumerable<Vector3Int> positions {
        get {
            for (int x = from.x; x <= to.x; x++) {
                for (int y = from.y; y <= to.y; y++) {
                    for (int z = from.z; z <= to.z; z++) {
                        yield return new(x, y, z);
                    }
                }
            }
        }
    }

    internal static Brick Parse(string line) {
        var positions = line
            .Replace('~', ',')
            .Split(',')
            .Select(int.Parse)
            .ToList();

        return new() {
            from = new(positions[0], positions[1], positions[2]),
            to = new(positions[3], positions[4], positions[5]),
        };
    }

    public override string ToString() => $"{from} ~ {to}";
}

static class Extensions {
}