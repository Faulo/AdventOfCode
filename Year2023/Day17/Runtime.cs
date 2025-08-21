using Utilities;
using HeatKey = (int x, int y, Utilities.Directions direction, int count);

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
        var openSet = new PriorityQueue<Node, int>();
        openSet.Enqueue(startNode, 0);

        var gScore = new Dictionary<Node, int>() {
            [startNode] = 0,
        };

        while (openSet.TryDequeue(out var current, out _)) {
            if (current.position == goal) {
                break;
            }

            foreach (var next in allDirections) {
                if (TryCreateNode(current, next, out var child)) {
                    if (!gScore.TryGetValue(child, out int score) || child.heatLossSum < score) {
                        gScore[child] = child.heatLossSum;
                        openSet.Enqueue(child, child.heatLossSum);
                    }
                }
            }
        }
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

    static readonly Directions[] allDirections = [
        Directions.Down,
        Directions.Right,
        Directions.Up,
        Directions.Left,
    ];

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

static class Extensions {
    internal static int AsInteger(this char character) {
        return character - '0';
    }
}