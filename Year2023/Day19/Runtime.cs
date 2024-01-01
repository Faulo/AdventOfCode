using System.Text.RegularExpressions;
using Utilities;
using PartRange = (Day19.Part min, Day19.Part max);
using Step = (char property, char operation, int value, string step);

namespace Day19;

sealed class Runtime {
    internal long sumOfAcceptedParts {
        get {
            long sum = 0;

            foreach (var part in parts) {
                if (IsAccepted(part)) {
                    sum += part.sum;
                }
            }

            return sum;
        }
    }

    internal long distinctCombinationsOfAcceptedParts {
        get {
            var ranges = FindAccepted()
                .ToList();

            long sum = 0;
            foreach (var range in ranges) {
                sum += range.GetCombinations();
            }

            return sum;
        }
    }

    internal readonly Dictionary<string, Workflow> workflows = [];
    internal readonly List<Part> parts = [];

    bool IsAccepted(Part part) {
        string name = "in";

        while (workflows.TryGetValue(name, out var workflow)) {
            name = workflow.Process(part);

            switch (name) {
                case "A":
                    return true;
                case "R":
                    return false;
            }
        }

        throw new ArgumentOutOfRangeException(name);
    }

    internal IEnumerable<PartRange> FindAccepted() {
        var range = (new Part(1, 1, 1, 1), new Part(4000, 4000, 4000, 4000));
        string name = "in";

        var ranges = new Queue<(PartRange range, string step)>();
        ranges.Enqueue((range, name));

        while (ranges.Count > 0) {
            (range, name) = ranges.Dequeue();
            var workflow = workflows[name];

            foreach (var item in workflow.GetRanges(range)) {
                switch (item.step) {
                    case "A":
                        yield return item.range;
                        break;
                    case "R":
                        break;
                    default:
                        ranges.Enqueue(item);
                        break;
                }
            }
        }
    }

    internal Runtime(string file) {
        bool isWorkflow = true;
        foreach (string line in new FileInput(file).ReadLines()) {
            if (string.IsNullOrWhiteSpace(line)) {
                isWorkflow = false;
                continue;
            }

            if (isWorkflow) {
                var workflow = Workflow.Parse(line);
                workflows[workflow.name] = workflow;
            } else {
                var part = Part.Parse(line);
                parts.Add(part);
            }
        }
    }
}

readonly struct Workflow {

    internal readonly string name;

    readonly Step[] steps;
    readonly string fallback;

    Workflow(string name, Step[] steps, string fallback) {
        this.name = name;
        this.steps = steps;
        this.fallback = fallback;
    }
    // xr{m>860:vl,m>809:ftq,A}
    internal static Workflow Parse(string line) {
        string[] values = line.Split('{');
        string name = values[0];

        values = values[1][..^1].Split(',');

        var steps = new Step[values.Length - 1];
        for (int i = 0; i < steps.Length; i++) {
            string[] step = values[i].Split(':');
            steps[i].property = step[0][0];
            steps[i].operation = step[0][1];
            steps[i].value = int.Parse(step[0][2..]);
            steps[i].step = step[1];
        }

        return new(name, steps, values[^1]);
    }

    internal IEnumerable<(PartRange range, string step)> GetRanges(PartRange range) {
        foreach (var step in steps) {
            if (step.TrySplitRange(range, out var ifRange, out var elseRange)) {
                if (ifRange.IsValid()) {
                    yield return (ifRange, step.step);
                }

                if (elseRange.IsValid()) {
                    range = elseRange;
                } else {
                    yield break;
                }
            }
        }

        yield return (range, fallback);
    }

    internal string Process(Part part) {
        for (int i = 0; i < steps.Length; i++) {
            int value = part[steps[i].property];
            bool isStep = steps[i].operation.Execute(value, steps[i].value);

            if (isStep) {
                return steps[i].step;
            }
        }

        return fallback;
    }
}

readonly struct Part(int x, int m, int a, int s) {
    internal readonly int x = x;
    internal readonly int m = m;
    internal readonly int a = a;
    internal readonly int s = s;

    internal int sum => x + m + a + s;

    internal int this[char property] => property switch {
        'x' => x,
        'm' => m,
        'a' => a,
        's' => s,
        _ => throw new NotImplementedException(),
    };

    // {x=787,m=2655,a=1222,s=2876}
    internal static Part Parse(string part) {
        var matches = Regex.Matches(part, "\\d+");
        var values = matches
            .Select(match => match.Value)
            .Select(int.Parse)
            .ToList();

        return new(values[0], values[1], values[2], values[3]);
    }

    internal Part With(char property, int value) {
        var (x, m, a, s) = (this.x, this.m, this.a, this.s);

        switch (property) {
            case 'x':
                x = value;
                break;
            case 'm':
                m = value;
                break;
            case 'a':
                a = value;
                break;
            case 's':
                s = value;
                break;
        };

        return new(x, m, a, s);
    }

    public override string ToString() => $"x={x},m={m},a={a},s={s}";

    public override bool Equals(object? obj) {
        return obj is Part other
            && other.x == x
            && other.m == m
            && other.a == a
            && other.s == s;
    }

    public override int GetHashCode() {
        return (x << 24) | (m << 16) | (a << 8) | s;
    }
}

static class Extensions {
    internal static bool TrySplitRange(this Step step, PartRange range, out PartRange ifRange, out PartRange elseRange) {
        switch (step.operation) {
            case '<':
                if (range.min[step.property] >= step.value || range.max[step.property] < step.value) {
                    ifRange = default;
                    elseRange = default;
                    return false;
                }

                (ifRange, elseRange) = range.SplitMax(step.property, step.value);
                return true;
            case '>':
                if (range.min[step.property] > step.value || range.max[step.property] <= step.value) {
                    ifRange = default;
                    elseRange = default;
                    return false;
                }

                (elseRange, ifRange) = range.SplitMin(step.property, step.value);
                return true;
            default:
                throw new NotImplementedException();
        }
    }
    internal static (PartRange, PartRange) SplitMax(this PartRange range, char property, int value) {
        return (
            (range.min, range.max.With(property, value - 1)),
            (range.min.With(property, value), range.max)
        );
    }
    internal static (PartRange, PartRange) SplitMin(this PartRange range, char property, int value) {
        return (
            (range.min, range.max.With(property, value)),
            (range.min.With(property, value + 1), range.max)
        );
    }
    internal static bool IsValid(this PartRange range) {
        return range.min.x <= range.max.x
            && range.min.m <= range.max.m
            && range.min.a <= range.max.a
            && range.min.s <= range.max.s;
    }
    internal static long GetCombinations(this PartRange range) {
        return ((long)range.max.x - range.min.x + 1)
             * ((long)range.max.m - range.min.m + 1)
             * ((long)range.max.a - range.min.a + 1)
             * ((long)range.max.s - range.min.s + 1);
    }
    internal static bool Execute(this char operation, int a, int b) => operation switch {
        '>' => a > b,
        '<' => a < b,
        _ => throw new NotImplementedException(),
    };
}