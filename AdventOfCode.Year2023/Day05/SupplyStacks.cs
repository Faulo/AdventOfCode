using System.Text;
using System.Text.RegularExpressions;

namespace Day05;

class SupplyStacks {
    const string INPUT_FOLDER = "input";

    public static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    public static int ParseStackCount(string file) {
        foreach (string line in ReadFileToArray(file)) {
            var matches = Regex.Matches(line, "\\d+");
            if (matches.Count > 0) {
                return matches.Count;
            }
        }
        throw new Exception("Failed to find any integers");
    }

    public static int ParseStackMaxHeight(string file) {
        int height = 0;
        foreach (string line in ReadFileToArray(file)) {
            var matches = Regex.Matches(line, "\\d+");
            if (matches.Count > 0) {
                return height;
            }
            height++;
        }
        throw new Exception("Failed to find any integers");
    }

    public static Dictionary<int, Stack<char>> ParseStacks(string file) {
        var stacks = new Dictionary<int, List<char>>();
        int count = ParseStackCount(file);
        int maxHeight = ParseStackMaxHeight(file);
        for (int i = 1; i <= count; i++) {
            stacks[i] = new();
        }
        foreach (string line in ReadFileToArray(file).Take(maxHeight)) {
            int cursor = 1;
            for (int i = 1; i <= count; i++) {
                char letter = line.Length > cursor
                    ? line[cursor]
                    : ' ';
                if (letter != ' ') {
                    stacks[i].Add(letter);
                }
                cursor += 4;
            }
        }
        for (int i = 1; i <= count; i++) {
            stacks[i].Reverse();
        }
        return stacks.ToDictionary(keyval => keyval.Key, keyval => new Stack<char>(keyval.Value));
    }

    public static IEnumerable<(int amount, int source, int target)> ParseMoves(string file) {
        int count = ParseStackCount(file);
        int maxHeight = ParseStackMaxHeight(file);
        foreach (string line in ReadFileToArray(file).Skip(maxHeight + 2)) {
            var matches = Regex.Matches(line, "\\d+");
            if (matches.Count == 3) {
                yield return (int.Parse(matches[0].Value), int.Parse(matches[1].Value), int.Parse(matches[2].Value));
            }
        }
    }

    public static void Execute((int amount, int source, int target) move, Dictionary<int, Stack<char>> stacks, Model model = Model.CrateMover9000) {
        switch (model) {
            case Model.CrateMover9000:
                for (int i = 0; i < move.amount; i++) {
                    stacks[move.target].Push(stacks[move.source].Pop());
                }
                break;
            case Model.CrateMover9001:
                var list = new List<char>();
                for (int i = 0; i < move.amount; i++) {
                    list.Add(stacks[move.source].Pop());
                }
                list.Reverse();
                foreach (char letter in list) {
                    stacks[move.target].Push(letter);
                }
                break;
            default:
                throw new Exception();
        }
    }

    public static string PrintStacks(Dictionary<int, Stack<char>> stacks) {
        var builder = new StringBuilder();
        foreach (var (index, stack) in stacks) {
            builder.Append(stack.Count == 0 ? ' ' : stack.Peek());
        }
        return builder.ToString();
    }

    public static string ExecuteAndPrint(string file, int? moveCount = null, Model model = Model.CrateMover9000) {
        var stacks = ParseStacks(file);
        var moves = ParseMoves(file);
        if (moveCount.HasValue) {
            moves = moves.Take(moveCount.Value);
        }
        foreach (var move in moves) {
            Execute(move, stacks, model);
        }
        return PrintStacks(stacks);
    }
}
