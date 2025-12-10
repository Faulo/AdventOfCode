using Utilities;

namespace Day10;

sealed partial class Runtime {
    sealed class IndicatorBox {
        readonly ulong targetLights;
        readonly ulong[] buttons;
        readonly int[][] buttonVoltages;
        readonly int[] targetVoltages;

        internal IndicatorBox(ulong targetLights, ulong[] buttons, int[] targetVoltages, int[][] buttonVoltages) {
            this.targetLights = targetLights;
            this.buttons = buttons;
            this.targetVoltages = targetVoltages;
            this.buttonVoltages = buttonVoltages;
        }

        internal int toggleCount {
            get {
                var range = Enumerable.Range(0, buttons.Length).ToList();

                var queue = new List<List<int>>(range.Select<int, List<int>>(i => [i]));

                for (int count = 0; count < buttons.Length; count++) {
                    if (count > 0) {
                        var newQueue = new List<List<int>>(queue.Count);
                        foreach (var indices in queue) {
                            foreach (int i in range) {
                                if (!indices.Contains(i)) {
                                    newQueue.Add([.. indices, i]);
                                }
                            }
                        }

                        queue = newQueue;
                    }

                    if (queue.AsParallel().Any(MatchesTargetLights)) {
                        Console.WriteLine($"{targetLights}: {count + 1}");
                        return count + 1;
                    }
                }

                throw new ApplicationException();
            }
        }

        bool MatchesTargetLights(List<int> indices) {
            ulong mask = 0;
            foreach (int i in indices) {
                mask ^= buttons[i];
            }

            return mask == targetLights;
        }

        internal int addCount {
            get {
                var range = Enumerable.Range(0, buttons.Length).ToList();

                var queue = new List<List<int>>([new int[buttons.Length].ToList()]);

                for (int count = 0; count < 13; count++) {
                    var newQueue = new List<List<int>>(queue.Count);
                    foreach (var indices in queue) {
                        if (indices.Count == 0) {
                            continue;
                        }

                        foreach (int i in range) {
                            List<int> list = new(indices);
                            list[i]++;
                            newQueue.Add(list);
                        }
                    }

                    queue = newQueue;

                    if (queue.AsParallel().Any(MatchesTargetVoltage)) {
                        Console.WriteLine($"{string.Join(',', targetVoltages)}: {count + 1}");
                        return count + 1;
                    }
                }

                throw new ApplicationException();
            }
        }

        bool MatchesTargetVoltage(List<int> indices) {
            Span<int> voltages = stackalloc int[targetVoltages.Length];

            for (int i = 0; i < indices.Count; i++) {
                Add(voltages, buttonVoltages[i], indices[i]);
            }

            int result = CompareTo(voltages, targetVoltages);

            if (result > 0) {
                indices.Clear();
            }

            return result == 0;
        }

        static void Clear(in Span<int> left) {
            for (int i = 0; i < left.Length; i++) {
                left[i] = 0;
            }
        }

        static void Add(in Span<int> left, IReadOnlyList<int> right, int multiplier) {
            for (int i = 0; i < left.Length; i++) {
                left[i] += right[i] * multiplier;
            }
        }

        static int CompareTo(in Span<int> left, IReadOnlyList<int> right) {
            bool matches = true;
            for (int i = 0; i < left.Length; i++) {
                switch (left[i].CompareTo(right[i])) {
                    case > 0:
                        return 1;
                    case < 0:
                        matches = false;
                        break;
                }
            }

            return matches
                ? 0
                : -1;
        }
    }

    internal static ulong ParseLights(string lights) {
        ulong mask = 0;
        for (int i = 0; i < lights.Length; i++) {
            if (lights[i] is '#') {
                mask |= (ulong)1 << i;
            }
        }

        return mask;
    }

    static ulong ParseButton(string button) {
        ulong mask = 0;
        foreach (string light in button.Split(',')) {
            mask |= (ulong)1 << int.Parse(light);
        }

        return mask;
    }

    internal static IEnumerable<ulong> ParseButtons(IReadOnlyList<string> buttons) {
        foreach (string button in buttons) {
            yield return ParseButton(button);
        }
    }

    internal static int[] ParseVoltages(string voltages) {
        return voltages
            .Split(',')
            .Select(int.Parse)
            .ToArray();
    }

    static int[] ParseButtonVoltage(string button, int length) {
        int[] buttonVoltage = new int[length];
        foreach (string light in button.Split(',')) {
            buttonVoltage[int.Parse(light)] = 1;
        }

        return buttonVoltage;
    }

    readonly List<IndicatorBox> boxes = [];

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] segments = line.Split(' ');

            ulong targetLights = ParseLights(segments[0][1..^1]);
            ulong[] buttons = segments
                .Skip(1)
                .Take(segments.Length - 2)
                .Select(s => s[1..^1])
                .Select(ParseButton)
                .ToArray();
            int[] targetVoltages = ParseVoltages(segments[^1][1..^1]);
            int[][] buttonVoltages = segments
                .Skip(1)
                .Take(segments.Length - 2)
                .Select(s => s[1..^1])
                .Select(s => ParseButtonVoltage(s, targetVoltages.Length))
                .ToArray();

            boxes.Add(new(targetLights, buttons, targetVoltages, buttonVoltages));
        }
    }

    internal int toggleCountSum => boxes.Sum(b => b.toggleCount);

    internal int addCountSum => boxes.Sum(b => b.addCount);
}