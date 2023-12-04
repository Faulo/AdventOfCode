using System.Text.RegularExpressions;
using Utilities;

namespace Day02;

sealed class Runtime(string file) {
    readonly Dictionary<string, int> cubes = new() {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14,
    };

    internal int sumOfPossible {
        get {
            int sum = 0;
            foreach (var game in games) {
                bool isValid = cubes
                    .All(cube => game.cubes.TryGetValue(cube.Key, out int amount) && cube.Value >= amount);

                if (isValid) {
                    sum += game.id;
                }
            }

            return sum;
        }
    }

    internal int sumOfProducts {
        get {

            int sum = 0;
            foreach (var game in games) {
                sum += game.cubes.Values.Aggregate(1, (i, j) => i * j);
            }

            return sum;
        }
    }

    IEnumerable<(int id, IReadOnlyDictionary<string, int> cubes)> games => new FileInput(file)
        .ReadLines()
        .Select(ParseLine);

    internal static (int id, IReadOnlyDictionary<string, int> cubes) ParseLine(string line) {
        var match = Regex.Match(line, "Game (\\d+):");
        int id = int.Parse(match.Groups[1].Value);

        var cubes = new Dictionary<string, int>();

        var matches = Regex.Matches(line, "(\\d+) ([a-z]+)");
        foreach (var m in matches.OfType<Match>()) {
            int amount = int.Parse(m.Groups[1].Value);
            string color = m.Groups[2].Value;

            if (cubes.TryGetValue(color, out int previous) && previous > amount) {
                continue;
            }

            cubes[color] = amount;
        }

        return (id, cubes);
    }
}