using System.Text.RegularExpressions;
using Utilities;

namespace Day09;

sealed class Runtime {
    internal record Race(long time, long distance) {
        internal long wins {
            get {
                return 1 + lastWin - firstWin;
            }
        }
        internal long firstWin {
            get {
                for (long i = 1; i < time; i++) {
                    if (i * (time - i) > distance) {
                        return i;
                    }
                }

                return -1;
            }
        }
        internal long lastWin {
            get {
                for (long i = time - 1; i > 0; i--) {
                    if (i * (time - i) > distance) {
                        return i;
                    }
                }

                return -1;
            }
        }
    }

    internal readonly List<Race> races = [];
    readonly string file;

    public long productOfWins => races
        .Aggregate((long)1, (p, r) => r.wins * p);

    internal Runtime(string file, bool badKerning = false) {
        this.file = file;

        var lines = new FileInput(file).ReadLines();
        string first = lines.First();
        string last = lines.Last();

        if (badKerning) {
            first = first.Replace(" ", "");
            last = last.Replace(" ", "");
        }

        var times = Regex.Matches(first, "(\\d+)");
        var distances = Regex.Matches(last, "(\\d+)");

        for (int i = 0; i < times.Count; i++) {
            races.Add(new(
                long.Parse(times[i].Groups[1].Value),
                long.Parse(distances[i].Groups[1].Value)
           ));
        }
    }
}