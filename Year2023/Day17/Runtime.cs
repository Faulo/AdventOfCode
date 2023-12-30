using Utilities;

namespace Day17;

sealed class Runtime {

    readonly CharacterMap map;
    readonly Vector2Int goal;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
        goal = new(map.width - 1, map.height - 1);
    }

    int currentHeatLoss;
    readonly Dictionary<(Vector2Int position, Directions direction), int> heatLossMap = [];

    bool useMultithreading = false;

    internal int mininumHeatLoss {
        get {
            currentHeatLoss = stubHeatLoss;
            heatLossMap.Clear();

            if (useMultithreading) {
                Parallel.ForEachAsync(new[] { Directions.Right }, async (direction, token) => {
                    await WalkAsync(direction.GetOffset(), direction, Enumerable.Empty<Vector2Int>(), token);
                }).Wait(300 * 1000);
            } else {
                WalkToGoal(Node.empty);
            }

            return currentHeatLoss;
        }
    }

    void WalkToGoal(Node node) {
        foreach (var next in allDirections) {
            if (TryCreateNode(node, next, out var child)) {
                if (child.position == goal) {
                    currentHeatLoss = child.heatLossSum;
                    // Console.WriteLine($"{currentHeatLoss}: {child}");
                } else {
                    WalkToGoal(child);
                }
            }
        }
    }

    bool TryCreateNode(Node node, Directions direction, out Node child) {
        if (direction == node.direction.GetOpposite()) {
            child = Node.empty;
            return false;
        }

        if (node.IsDirectionCount(direction, 3)) {
            child = Node.empty;
            return false;
        }

        var position = node.position + direction.GetOffset();

        if (!map.IsInBounds(position)) {
            child = Node.empty;
            return false;
        }

        if (node.ContainsPosition(position)) {
            child = Node.empty;
            return false;
        }

        var key = (position, direction);

        child = new(node, position, direction, map[position].AsInteger());

        if (heatLossMap.TryGetValue(key, out int heatLoss)) {
            if (heatLoss < child.heatLossSum) {
                return false;
            }
        }

        heatLossMap[key] = child.heatLossSum;

        return child.heatLossSum < currentHeatLoss;
    }

    void Walk(Vector2Int position, Directions direction, IEnumerable<Vector2Int> positions, int heatLoss = 0, int directionCount = 0) {
        if (!map.IsInBounds(position)) {
            return;
        }

        heatLoss += map[position].AsInteger();

        if (heatLoss >= currentHeatLoss) {
            return;
        }

        if (position == goal) {
            currentHeatLoss = heatLoss;
            Console.WriteLine(heatLoss + ": " + string.Join('>', positions.Select(p => (p.x, p.y))));
            return;
        }

        var newPositions = new HashSet<Vector2Int>(positions);
        if (!newPositions.Add(position)) {
            return;
        }

        var opposite = direction.GetOpposite();

        foreach (var next in allDirections) {
            if (opposite != next) {
                int count = direction == next
                    ? directionCount + 1
                    : 0;
                if (count < 3) {
                    Walk(position + next.GetOffset(), next, newPositions, heatLoss, count);
                }
            }
        }

        return;
    }

    async Task WalkAsync(Vector2Int position, Directions direction, IEnumerable<Vector2Int> positions, CancellationToken token, int heatLoss = 0, int directionCount = 0) {
        if (token.IsCancellationRequested) {
            return;
        }

        if (!map.IsInBounds(position)) {
            return;
        }

        heatLoss += map[position].AsInteger();

        lock (this) {
            if (heatLoss >= currentHeatLoss) {
                return;
            }

            if (position == goal) {
                currentHeatLoss = heatLoss;
                Console.WriteLine(heatLoss + ": " + string.Join('>', positions.Select(p => (p.x, p.y))));
                return;
            }
        }

        var newPositions = new HashSet<Vector2Int>(positions);
        if (!newPositions.Add(position)) {
            return;
        }

        var opposite = direction.GetOpposite();

        await Parallel.ForEachAsync(allDirections, token, async (next, token) => {
            if (opposite != next) {
                int count = direction == next
                    ? directionCount + 1
                    : 0;
                if (count < 3) {
                    await WalkAsync(position + next.GetOffset(), next, newPositions, token, heatLoss, count);
                }
            }
        });
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

    public Node(Node? parent, Vector2Int position, Directions direction, int heatLoss) {
        this.parent = parent;
        this.position = position;
        this.direction = direction;
        this.heatLoss = heatLoss;

        heatLossSum = parent is null
            ? heatLoss
            : heatLoss + parent.heatLossSum;
    }

    internal static readonly Node empty = new(null, new(0, 0), Directions.None, 0);

    internal bool IsDirectionCount(Directions direction, int count) {
        var node = this;
        for (int i = 0; i < count; i++) {
            if (node is null || node.direction != direction) {
                return false;
            }

            node = node.parent;
        }

        return true;
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