using System.Text.RegularExpressions;
using Utilities;
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

    internal long distinctCombinationsOfAcceptedParts => 0;

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
}

static class Extensions {
    internal static bool Execute(this char operation, int a, int b) => operation switch {
        '>' => a > b,
        '<' => a < b,
        _ => throw new NotImplementedException(),
    };
}