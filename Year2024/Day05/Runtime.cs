using Utilities;

namespace Day05;

sealed partial class Runtime {

    internal readonly List<(int left, int right)> rules = [];
    internal readonly List<int[]> updates = [];

    internal Runtime(string file) {
        var input = new FileInput(file).ReadLines();

        bool first = true;
        foreach (string row in input) {
            if (string.IsNullOrEmpty(row)) {
                first = false;
                continue;
            }

            if (first) {
                if (row.Split('|') is [string left, string right]) {
                    rules.Add((int.Parse(left), int.Parse(right)));
                }
            } else {
                updates.Add(row.Split(',').Select(int.Parse).ToArray());
            }
        }
    }

    internal bool IsValid(int[] update) {
        foreach (var rule in rules) {
            if (!IsValid(update, rule)) {
                return false;
            }
        }

        return true;
    }

    bool IsValid(int[] update, (int left, int right) rule) {
        for (int i = 0; i < update.Length; i++) {
            if (update[i] == rule.left) {
                for (int j = 0; j < i; j++) {
                    if (update[j] == rule.right) {
                        return false;
                    }
                }
            }

            if (update[i] == rule.right) {
                for (int j = i + 1; j < update.Length; j++) {
                    if (update[j] == rule.left) {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    internal int sumOfMiddle {
        get {
            return updates
                .Where(IsValid)
                .Sum(u => u[u.Length / 2]);
        }
    }
}