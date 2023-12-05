using System.Text.RegularExpressions;
using Utilities;

namespace Day05;

sealed class Runtime {
    internal record Map(long destination, long source, long count) {
        internal bool TryTranslate(ref long input) {
            if (source <= input && input < (source + count)) {
                input = destination + (input - source);
                return true;
            }

            return false;
        }
    }

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
                        long.Parse(match.Groups[1].Value),
                        long.Parse(match.Groups[2].Value),
                        long.Parse(match.Groups[3].Value)
                   ));
                }
            }
        }
    }

    public IEnumerable<long> seeds {
        get {
            foreach (string line in lines) {
                var match = Regex.Match(line, "seeds: ([\\s\\d]+)");
                if (match.Success) {
                    foreach (string seed in match.Groups[1].Value.Split(' ')) {
                        if (long.TryParse(seed, out long result)) {
                            yield return result;
                        }
                    }
                }
            }
        }
    }

    public IEnumerable<long> seedsOfPairs {
        get {
            foreach (string line in lines) {
                var match = Regex.Match(line, "seeds: ([\\s\\d]+)");
                if (match.Success) {
                    bool first = true;
                    long start = 0;
                    long count = 0;
                    foreach (string seed in match.Groups[1].Value.Split(' ')) {
                        if (long.TryParse(seed, out long result)) {
                            if (first) {
                                first = false;
                                start = result;
                            } else {
                                first = true;
                                count = result;

                                for (long i = 0; i < count; i++) {
                                    yield return start + i;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    internal long lowestLocation => seeds
        .Select(Translate)
        .Min();

    internal long lowestLocationOfPairs => seedsOfPairs
        .Select(Translate)
        .Min();

    internal long Translate(long seed) {
        foreach (var maps in maps.Values) {
            foreach (var map in maps) {
                if (map.TryTranslate(ref seed)) {
                    break;
                }
            }
        }

        return seed;
    }
}