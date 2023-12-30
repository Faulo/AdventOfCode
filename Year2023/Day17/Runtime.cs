using Utilities;
using HeatKey = (int x, int y, Day17.Directions direction, int count);

namespace Day17;

sealed class Runtime {
    static readonly bool logOutput = false;

    readonly CharacterMap map;
    readonly Vector2Int goal;

    readonly int minDirectionCount;
    readonly int maxDirectionCount;

    internal Runtime(string file, bool isUltra = false) {
        map = new FileInput(file).ReadAllAsCharacterMap();
        goal = new(map.width - 1, map.height - 1);

        if (isUltra) {
            minDirectionCount = 4;
            maxDirectionCount = 10;
        } else {
            minDirectionCount = 0;
            maxDirectionCount = 3;
        }
    }

    int currentHeatLoss;

    internal int mininumHeatLoss {
        get {
            currentHeatLoss = 10 * map.width * map.height;

            AStar(Node.empty);

            return currentHeatLoss;
        }
    }

    void AStar(Node startNode) {
        var openSet = new HashSet<Node>() {
            startNode
        };

        var gScore = new Dictionary<Node, int>() {
            [startNode] = 0,
        };

        do {
            var current = openSet
                .OrderBy(n => n.heatLossSum)
                .First();

            if (current.position == goal) {
                break;
            }

            openSet.Remove(current);

            foreach (var next in allDirections) {
                if (TryCreateNode(current, next, out var child)) {
                    if (!gScore.TryGetValue(child, out int score) || child.heatLossSum < score) {
                        gScore[child] = child.heatLossSum;
                        openSet.Add(child);
                    }
                }
            }
        } while (openSet.Count > 0);
    }

    bool TryCreateNode(Node node, Directions direction, out Node child) {
        if (direction == node.direction.GetOpposite()) {
            child = Node.empty;
            return false;
        }

        int count = node.GetDirectionCount(direction);
        if (count == maxDirectionCount) {
            child = Node.empty;
            return false;
        }

        var position = node.position + direction.GetOffset();

        if (!map.IsInBounds(position)) {
            child = Node.empty;
            return false;
        }

        child = new(node, position, direction, map[position].AsInteger());

        if (node.direction != Directions.None && node.direction != direction) {
            count = node.GetDirectionCount(node.direction);
            if (count < minDirectionCount) {
                child = Node.empty;
                return false;
            }

            for (int i = 1; i < minDirectionCount; i++) {
                position += direction.GetOffset();
                if (!map.IsInBounds(position)) {
                    return false;
                }

                child = new(child, position, direction, map[position].AsInteger());
            }
        }

        if (child.position == goal) {
            if (currentHeatLoss > child.heatLossSum) {
                currentHeatLoss = child.heatLossSum;

                if (logOutput) {
                    Console.WriteLine(currentHeatLoss);
                    Console.WriteLine(child);
                }

                return true;
            }

            return false;
        }

        return currentHeatLoss > child.heatLossSum;
    }

    static readonly Directions[] allDirections = {
        Directions.Down,
        Directions.Right,
        Directions.Up,
        Directions.Left,
    };

    internal int stubHeatLoss {
        get {
            int sum = 0;

            for (int i = 1; i < map.width; i++) {
                sum += map[i, i - 1].AsInteger();
                sum += map[i, i].AsInteger();
            }

            return sum;
        }
    }
}

class Node {
    internal readonly Node? parent;
    internal readonly Vector2Int position;
    internal readonly Directions direction;
    internal readonly int heatLoss;
    internal readonly int heatLossSum;
    internal readonly HeatKey heatKey;

    public Node(Node? parent, Vector2Int position, Directions direction, int heatLoss) {
        this.parent = parent;
        this.position = position;
        this.direction = direction;
        this.heatLoss = heatLoss;

        heatLossSum = parent is null
            ? heatLoss
            : heatLoss + parent.heatLossSum;

        heatKey = (
            position.x,
            position.y,
            direction,
            GetDirectionCount(direction)
        );
    }

    public override bool Equals(object? obj) {
        return obj is Node other && heatKey == other.heatKey;
    }

    public override int GetHashCode() => HashCode.Combine(heatKey.x, heatKey.y, heatKey.direction, heatKey.count);

    internal static readonly Node empty = new(null, new(0, 0), Directions.None, 0);

    internal int GetDirectionCount(Directions direction) {
        var node = this;
        for (int i = 0; ; i++) {
            if (node is null || node.direction != direction) {
                return i;
            }

            node = node.parent;
        }
    }

    internal bool ContainsPosition(Vector2Int position) {
        for (var node = this; node is not null; node = node.parent) {
            if (node.position == position) {
                return true;
            }
        }

        return false;
    }

    IEnumerable<Node> ancestors {
        get {
            for (var node = this; node is not null; node = node.parent) {
                yield return node;
            }
        }
    }

    public override string ToString() {
        return string.Join(
            string.Empty,
            ancestors.Reverse().Select(node => node.direction.ToCharacter() + (node.position.x, node.position.y))
        );
    }
}

[Flags]
public enum Directions {
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    All = Up | Down | Left | Right,
}

static class Extensions {
    internal static IEnumerable<T> AsEnumerable<T>(this T[,] map) {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                yield return map[x, y];
            }
        }
    }
    internal static int AsInteger(this char character) {
        return character - '0';
    }
    internal static Directions GetOpposite(this Directions direction) {
        return direction switch {
            Directions.Up => Directions.Down,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Right,
            Directions.Right => Directions.Left,
            _ => Directions.None,
        };
    }
    internal static Vector2Int GetOffset(this Directions direction) {
        return direction switch {
            Directions.Up => Vector2Int.up,
            Directions.Down => Vector2Int.down,
            Directions.Left => Vector2Int.left,
            Directions.Right => Vector2Int.right,
            _ => new(0, 0),
        };
    }
    internal static string ToCharacter(this Directions direction) {
        return direction switch {
            Directions.Up => "^",
            Directions.Down => "v",
            Directions.Left => "<",
            Directions.Right => ">",
            _ => "o",
        };
    }
}