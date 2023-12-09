using System.Text.RegularExpressions;
using Utilities;

namespace Day08;

public sealed class Runtime {
    public enum Direction {
        Left,
        Right,
    }
    internal record Node(string name) {
        internal Node? left;
        internal Node? right;
        internal Node this[Direction direction] {
            get {
                return direction == Direction.Left
                    ? left!
                    : right!;
            }
        }
        internal readonly bool isStart = name[^1] == 'A';
        internal readonly bool isGoal = name[^1] == 'Z';
    }

    internal long numberOfSteps {
        get {
            var node = nodes["AAA"];
            var goal = nodes["ZZZ"];
            long steps = 0;

            foreach (var direction in infiniteInstructions) {
                if (node == goal) {
                    break;
                }

                node = node[direction];
                steps++;
            }

            return steps;
        }
    }
    internal long numberOfGhostSteps {
        get {
            var nodes = this.nodes
                .Values
                .Where(n => n.isStart)
                .ToArray();
            long steps = 0;

            foreach (var direction in infiniteInstructions) {
                bool isGoal = true;
                for (int i = 0; i < nodes.Length; i++) {
                    nodes[i] = nodes[i][direction];
                    if (!nodes[i].isGoal) {
                        isGoal = false;
                    }
                }

                steps++;

                if (isGoal) {
                    break;
                }
            }

            return steps;
        }
    }

    internal IEnumerable<Direction> infiniteInstructions {
        get {
            for (int i = 0; ; i++) {
                if (i == instructions.Count) {
                    i = 0;
                }

                yield return instructions[i];
            }
        }
    }
    internal readonly IReadOnlyList<Direction> instructions;

    internal readonly Dictionary<string, Node> nodes = [];
    Node GetOrAdd(string name) {
        return nodes.TryGetValue(name, out var node)
            ? node
            : nodes[name] = new(name);
    }

    internal static IEnumerable<Direction> ParseDirections(string instructions) => instructions
        .ToCharArray()
        .Select(c => c == 'L' ? Direction.Left : Direction.Right);

    internal Runtime(string file) {
        var lines = new FileInput(file).ReadLines();
        instructions = ParseDirections(lines.First()).ToArray();

        foreach (string? line in lines.Skip(1)) {
            var match = Regex.Match(line, "(\\w+) = \\((\\w+), (\\w+)\\)");
            if (match.Success) {
                string name = match.Groups[1].Value;
                string left = match.Groups[2].Value;
                string right = match.Groups[3].Value;

                var node = GetOrAdd(name);
                node.left = GetOrAdd(left);
                node.right = GetOrAdd(right);
            }
        }
    }
}