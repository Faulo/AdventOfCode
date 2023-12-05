using System.Text.RegularExpressions;
using Utilities;

namespace Day05;

sealed class Runtime {
    internal record Map(int destination, int source, int count);

    readonly IEnumerable<string> lines;
    internal readonly Dictionary<string, List<Map>> maps = [];
    readonly string file;

    internal Runtime(string file) {
        this.file = file;

        lines = new FileInput(file).ReadLines();

        string key = "";
        foreach (string line in lines) {
            var match = Regex.Match(line, "(.+) map:");
            if (match.Success) {
                key = match.Groups[1].Value;
                maps[key] = [];
            }

            if (!string.IsNullOrEmpty(key)) {
                match = Regex.Match(line, "([\\d]+) ([\\d]+) ([\\d]+)");

                if (match.Success) {
                    maps[key].Add(new(
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value)
                   ));
                }
            }
        }
    }

    public IEnumerable<int> seeds {
        get {
            foreach (string line in lines) {
                var match = Regex.Match(line, "seeds: ([\\s\\d]+)");
                if (match.Success) {
                    foreach (string seed in match.Groups[1].Value.Split(' ')) {
                        if (int.TryParse(seed, out int result)) {
                            yield return result;
                        }
                    }
                }
            }
        }
    }

    internal int lowestLocation { get; }

    internal bool GetSeedLocation(int seed) => throw new NotImplementedException();
}