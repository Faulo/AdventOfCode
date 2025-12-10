using System.Buffers;
using System.Collections.Concurrent;
using Utilities;

namespace Day10;

sealed partial class Runtime {
    sealed class IndicatorBox {
        readonly ulong targetLights;
        readonly ulong[] buttons;
        readonly int[][] buttonVoltages;
        readonly int[] targetVoltages;
        readonly int[] range;

        internal IndicatorBox(ulong targetLights, ulong[] buttons, int[] targetVoltages, int[][] buttonVoltages) {
            this.targetLights = targetLights;
            this.buttons = buttons;
            this.targetVoltages = targetVoltages;
            this.buttonVoltages = buttonVoltages;

            range = Enumerable.Range(0, buttons.Length).ToArray();
        }

        internal int toggleCount {
            get {

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

        const bool USE_POOL = false;

        int[] Rent() {
            return USE_POOL
                ? ArrayPool<int>.Shared.Rent(buttons.Length)
                : new int[buttons.Length];
        }

        void Return(int[] indices) {
            ArrayPool<int>.Shared.Return(indices);
        }

        ConcurrentBag<int[]> newQueue = [];

        internal int addCount {
            get {
                int[] start = Rent();
                Array.Fill(start, 0, 0, buttons.Length);
                ConcurrentBag<int[]> queue = [start];

                for (int count = 0; count < 13; count++) {
                    newQueue = [];

                    bool isDone = queue
                        .AsParallel()
                        .Any(MatchesTargetVoltage);

                    if (USE_POOL) {
                        queue.AsParallel().ForAll(Return);
                    }

                    if (isDone) {
                        if (USE_POOL) {
                            newQueue.AsParallel().ForAll(Return);
                        }

                        Console.WriteLine($"{string.Join(',', targetVoltages)}: {count}");
                        return count;
                    }

                    queue = newQueue;
                }

                throw new ApplicationException();
            }
        }

        bool MatchesTargetVoltage(int[] indices) {
            switch (CompareToTargetVoltage(indices)) {
                case > 0:
                    break;
                case 0:
                    return true;
                case < 0:
                    foreach (int i in range) {
                        int[] newIndices = Rent();
                        Array.Copy(indices, newIndices, buttons.Length);
                        newIndices[i]++;
                        newQueue.Add(newIndices);
                    }

                    break;
            }

            return false;
        }

        int CompareToTargetVoltage(int[] indices) {
            Span<int> voltages = stackalloc int[targetVoltages.Length];
            voltages.Clear();

            for (int i = 0; i < buttons.Length; i++) {
                Add(voltages, buttonVoltages[i], indices[i]);
            }

            return CompareTo(voltages, targetVoltages);
        }

        static void Add(in Span<int> left, int[] right, int multiplier) {
            for (int i = 0; i < left.Length; i++) {
                left[i] += right[i] * multiplier;
            }
        }

        static int CompareTo(in Span<int> left, int[] right) {
            int matches = 0;
            for (int i = 0; i < left.Length; i++) {
                switch (left[i].CompareTo(right[i])) {
                    case > 0:
                        return 1;
                    case < 0:
                        matches = -1;
                        break;
                }
            }

            return matches;
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

    readonly IndicatorBox[] boxes;

    internal Runtime(string file) {
        boxes = new FileInput(file)
            .ReadLines()
            .Select(line => {
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

                return new IndicatorBox(targetLights, buttons, targetVoltages, buttonVoltages);
            })
            .ToArray();
    }

    internal int toggleCountSum => boxes.AsParallel().Sum(b => b.toggleCount);

    internal int addCountSum => boxes.AsParallel().Sum(b => b.addCount);
}