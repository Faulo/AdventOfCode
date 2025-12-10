using Utilities;

namespace Day10;

sealed partial class Runtime {
    sealed class IndicatorBox {
        readonly ulong targetLights;
        readonly ulong[] buttons;

        internal IndicatorBox(ulong targetLights, ulong[] buttons) {
            this.targetLights = targetLights;
            this.buttons = buttons;
        }

        internal int pressCount {
            get {
                var range = Enumerable.Range(0, buttons.Length).ToList();

                IEnumerable<List<int>> queue = new List<List<int>>(range.Select<int, List<int>>(i => [i]));

                for (int count = 0; count < buttons.Length; count++) {
                    if (count > 0) {
                        queue = queue
                            .SelectMany(indices => range
                                .Where(i => !indices.Contains(i))
                                .Select(i => indices.Append(i).ToList())
                            );
                    }

                    foreach (var indices in queue) {
                        ulong mask = 0;
                        foreach (int i in indices) {
                            mask ^= buttons[i];
                        }

                        if (mask == targetLights) {
                            return count + 1;
                        }
                    }
                }

                throw new ApplicationException();
            }
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

            boxes.Add(new(targetLights, buttons));
        }
    }

    internal int pressCountSum => boxes.Sum(b => b.pressCount);
}