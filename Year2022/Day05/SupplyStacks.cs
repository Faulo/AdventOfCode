using System.Text;
using System.Text.RegularExpressions;
using Utilities;

namespace Day05;

class SupplyStacks {
    readonly FileInput input;
    readonly Model model;

    internal SupplyStacks(string file, Model model = Model.CrateMover9000) {
        input = new(file);
        this.model = model;
    }

    internal IEnumerable<string> ReadFileToArray() {
        return input.ReadLines();
    }

    internal int ParseStackCount() {
        foreach (string line in ReadFileToArray()) {
            var matches = Regex.Matches(line, "\\d+");
            if (matches.Count > 0) {
                return matches.Count;
            }
        }
        throw new Exception("Failed to find any integers");
    }

    internal int ParseStackMaxHeight() {
        int height = 0;
        foreach (string line in ReadFileToArray()) {
            var matches = Regex.Matches(line, "\\d+");
            if (matches.Count > 0) {
                return height;
            }
            height++;
        }
        throw new Exception("Failed to find any integers");
    }

    internal Dictionary<int, Stack<char>> ParseStacks() {
        var stacks = new Dictionary<int, List<char>>();
        int count = ParseStackCount();
        int maxHeight = ParseStackMaxHeight();
        for (int i = 1; i <= count; i++) {
            stacks[i] = new();
        }
        foreach (string line in ReadFileToArray().Take(maxHeight)) {
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

    internal IEnumerable<(int amount, int source, int target)> ParseMoves() {
        int count = ParseStackCount();
        int maxHeight = ParseStackMaxHeight();
        foreach (string line in ReadFileToArray().Skip(maxHeight + 2)) {
            var matches = Regex.Matches(line, "\\d+");
            if (matches.Count == 3) {
                yield return (int.Parse(matches[0].Value), int.Parse(matches[1].Value), int.Parse(matches[2].Value));
            }
        }
    }

    internal string ExecuteAndPrint(int? moveCount = null) {
        var stacks = ParseStacks();
        var moves = ParseMoves();
        if (moveCount.HasValue) {
            moves = moves.Take(moveCount.Value);
        }
        foreach (var move in moves) {
            Execute(move, stacks, model);
        }
        return PrintStacks(stacks);
    }

    internal static void Execute((int amount, int source, int target) move, Dictionary<int, Stack<char>> stacks, Model model = Model.CrateMover9000) {
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

    internal static string PrintStacks(Dictionary<int, Stack<char>> stacks) {
        var builder = new StringBuilder();
        foreach (var (index, stack) in stacks) {
            builder.Append(stack.Count == 0 ? ' ' : stack.Peek());
        }
        return builder.ToString();
    }
}
