using System.Text.RegularExpressions;
using Utilities;

namespace Day04;

sealed class Runtime(string file) {
    readonly IEnumerable<string> lines = new FileInput(file).ReadLines();

    internal int sumOfWins => lines
        .Select(CalculateWins)
        .Select(win => win.count)
        .Select(CalculatePowerOfWins)
        .Sum();

    internal int sumOfCards {
        get {
            var counts = new Dictionary<int, int>();
            foreach (var (id, _) in lines.Select(CalculateWins)) {
                counts[id] = 1;
            }

            foreach (var (id, count) in lines.Select(CalculateWins)) {
                foreach (int j in Enumerable.Range(1, count)) {
                    counts[id + j] += counts[id];
                }
            }

            return counts.Values.Sum();
        }
    }

    internal static (int id, int count) CalculateWins(string line) {
        var match = Regex.Match(line, "Card\\s+(\\d+):(.+)\\|(.+)");

        int id = int.Parse(match.Groups[1].Value);

        var wins = SplitNumbers(match.Groups[2].Value);
        var owns = SplitNumbers(match.Groups[3].Value);

        return (id, wins.Intersect(owns).Count());
    }

    internal static int CalculatePowerOfWins(int wins) {
        return wins == 0
            ? 0
            : (int)Math.Pow(2, wins - 1);
    }

    static IEnumerable<int> SplitNumbers(string numbers) => numbers
            .Trim()
            .Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse);
}