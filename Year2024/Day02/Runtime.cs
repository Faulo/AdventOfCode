using System.Text.RegularExpressions;
using Utilities;

namespace Day01;

sealed partial class Runtime {
    internal readonly int[][] map;

    [GeneratedRegex("\\d+", RegexOptions.Compiled)]
    private static partial Regex NumberExpression();

    internal Runtime(string file) {
        map = new FileInput(file)
            .ReadLines()
            .Select(line => NumberExpression().Matches(line).Select(m => int.Parse(m.Value)).ToArray())
            .ToArray();
    }

    internal static int SafeScore(int left, int right) {
        return (right - left) switch {
            < -3 or 0 or > 3 => 0,
            > 0 => 1,
            < 0 => -1,
        };
    }

    internal int safeReports {
        get {
            int sum = 0;

            foreach (int[] row in map) {
                int direction = SafeScore(row[0], row[1]);
                int count = Math.Abs(direction);

                for (int i = 2; i < row.Length; i++) {
                    if (SafeScore(row[i - 1], row[i]) != direction) {
                        count = 0;
                        break;
                    }
                }

                sum += count;
            }

            return sum;
        }
    }
}