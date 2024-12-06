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
        return IsValid(update, false);
    }

    bool IsValid(int[] update, bool fixUpdate) {
        foreach (var rule in rules) {
            if (!IsValid(update, rule, fixUpdate)) {
                return false;
            }
        }

        return true;
    }

    static bool IsValid(int[] update, (int left, int right) rule, bool fixUpdate) {
        for (int i = 0; i < update.Length; i++) {
            if (update[i] == rule.left) {
                for (int j = 0; j < i; j++) {
                    if (update[j] == rule.right) {
                        if (fixUpdate) {
                            (update[i], update[j]) = (update[j], update[i]);
                        }

                        return false;
                    }
                }
            }

            if (update[i] == rule.right) {
                for (int j = i + 1; j < update.Length; j++) {
                    if (update[j] == rule.left) {
                        if (fixUpdate) {
                            (update[i], update[j]) = (update[j], update[i]);
                        }

                        return false;
                    }
                }
            }
        }

        return true;
    }

    internal bool FixValid(int[] update) {
        if (IsValid(update)) {
            return false;
        }

        for (int i = 0; i < 1000; i++) {
            if (IsValid(update, true)) {
                return true;
            }
        }

        throw new Exception($"Unfixable update: {string.Join(',', update)}");
    }

    internal int sumOfCorrectMiddle {
        get {
            return updates
                .Where(IsValid)
                .Sum(u => u[u.Length >> 1]);
        }
    }

    internal int sumOfWrongMiddle {
        get {
            return updates
                .Where(FixValid)
                .Sum(u => u[u.Length >> 1]);
        }
    }
}