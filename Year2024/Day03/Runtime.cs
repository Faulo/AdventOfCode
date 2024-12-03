using System.Text.RegularExpressions;
using Utilities;

namespace Day03;

sealed partial class Runtime {
    internal readonly (string name, int left, int right)[] instructions;

    [GeneratedRegex("(mul)\\((\\d{1,3}),(\\d{1,3})\\)", RegexOptions.Compiled)]
    private static partial Regex NumberExpression();

    internal Runtime(string file) {
        instructions = new FileInput(file)
            .ReadLines()
            .SelectMany(line => NumberExpression().Matches(line).Select(m => (m.Groups[1].Value, int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value))))
            .ToArray();
    }

    int Run((string name, int left, int right) instruction) {
        return instruction switch {
            ("mul", int a, int b) => a * b,
            _ => 0,
        };
    }

    internal int multSum {
        get {
            return instructions.Sum(Run);
        }
    }
}