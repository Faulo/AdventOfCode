using System.Text.RegularExpressions;
using Utilities;

namespace Day06;

sealed class Runtime {
    internal record Race(int time, int distance) {
        internal int wins {
            get {
                return 0;
            }
        }
    }

    internal readonly List<Race> races = [];
    readonly string file;

    public int productOfWins => races
        .Aggregate(1, (p, r) => r.wins * p);

    internal Runtime(string file) {
        this.file = file;

        var lines = new FileInput(file).ReadLines();
        var times = Regex.Matches(lines.First(), "(\\d+)");
        var distances = Regex.Matches(lines.Last(), "(\\d+)");

        for (int i = 0; i < times.Count; i++) {
            races.Add(new(
                int.Parse(times[i].Groups[1].Value),
                int.Parse(distances[i].Groups[1].Value)
           ));
        }
    }
}