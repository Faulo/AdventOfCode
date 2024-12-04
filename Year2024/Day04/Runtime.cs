using System.Text.RegularExpressions;
using Utilities;

namespace Day04;

sealed partial class Runtime {
    readonly (string name, int left, int right)[] instructions;

    readonly bool useDo;

    [GeneratedRegex("(mul|do|don't)\\((\\d{0,3}),?(\\d{0,3})\\)", RegexOptions.Compiled)]
    private static partial Regex NumberExpression();

    internal Runtime(string file, bool useDo = false) {
        instructions = new FileInput(file)
            .ReadLines()
            .SelectMany(line => NumberExpression().Matches(line).Select(m => (m.Groups[1].Value, int.TryParse(m.Groups[2].Value, out int left) ? left : 0, int.TryParse(m.Groups[3].Value, out int right) ? right : 0)))
            .ToArray();
        this.useDo = useDo;
    }

    bool canMult = true;

    int Run((string name, int left, int right) instruction) {
        return instruction switch {
            ("mul", int a, int b) when canMult => a * b,
            ("do", _, _) when useDo => SetMult(true),
            ("don't", _, _) when useDo => SetMult(false),
            _ => 0,
        };
    }

    int SetMult(bool canMult) {
        this.canMult = canMult;
        return 0;
    }

    internal int occurences {
        get {
            return instructions.Sum(Run);
        }
    }
}