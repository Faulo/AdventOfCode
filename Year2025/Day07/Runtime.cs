using Utilities;

namespace Day07;

sealed partial class Runtime {
    readonly List<(long start, long end)> ranges = [];
    readonly List<long> ingredients = [];

    internal Runtime(string file) {
        bool processRanges = true;
        foreach (string line in new FileInput(file).ReadLines()) {
            if (string.IsNullOrEmpty(line)) {
                processRanges = false;
                continue;
            }

            if (processRanges) {
                string[] rangeText = line.Split('-');
                (long start, long end) range = (long.Parse(rangeText[0]), long.Parse(rangeText[1]));
                ranges.Add(range);
            } else {
                ingredients.Add(long.Parse(line));
            }
        }

        CollapseRanges();
    }

    void CollapseRanges() {
        bool hasChanged;
        do {
            hasChanged = false;
            for (int i = 0; i < ranges.Count; i++) {
                var (start, end) = ranges[i];
                for (int j = 0; j < ranges.Count; j++) {
                    if (i == j) {
                        continue;
                    }

                    var r = ranges[j];
                    if (r.start >= start && r.start <= end) {
                        ranges[i] = (start, Math.Max(r.end, end));
                        ranges.RemoveAt(j);
                        hasChanged = true;
                        i--;
                        break;
                    }
                }
            }
        } while (hasChanged);
    }

    internal int freshIngredients => ingredients.Count(IsFresh);

    internal long allFreshIngredients => ranges.Sum(r => r.end - r.start + 1);

    bool IsFresh(long id) {
        foreach (var (start, end) in ranges) {
            if (id >= start && id <= end) {
                return true;
            }
        }

        return false;
    }
}