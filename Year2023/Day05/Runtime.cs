using System.Text.RegularExpressions;
using Utilities;
using Pair = (long start, long count);
using TranslatedPair = ((long start, long count) pair, bool isTranslated);

namespace Day05;

sealed class Runtime {
    internal record Map(long destination, long source, long count) {
        internal bool CanTranslate(long input)
            => source <= input && input < (source + count);
        internal long Translate(long input)
            => destination + (input - source);
        internal IEnumerable<TranslatedPair> TranslatePair(Pair value) {
            long count = source - value.start;
            if (count > 0) {
                count = Math.Min(count, value.count);

                yield return ((value.start, count), false);

                value.start += count;
                value.count -= count;
            }

            if (CanTranslate(value.start)) {
                count = Math.Min(value.count, source - value.start + this.count);
                if (count > 0) {
                    yield return ((Translate(value.start), count), true);

                    value.start += count;
                    value.count -= count;
                }
            }

            count = value.count;
            if (count > 0) {
                yield return ((value.start, count), false);
            }
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

    internal IEnumerable<long> seeds {
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

    internal IEnumerable<Pair> pairsOfSeeds {
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

                                yield return (start, count);
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

    internal long lowestLocationOfPairs => pairsOfSeeds
        .SelectMany(Translate)
        .Select(p => p.start)
        .Min();

    internal long Translate(long seed) {
        foreach (var maps in maps.Values) {
            foreach (var map in maps) {
                if (map.CanTranslate(seed)) {
                    seed = map.Translate(seed);
                    break;
                }
            }
        }

        return seed;
    }

    internal IEnumerable<Pair> Translate(Pair seed) {
        List<Pair> inputs = [seed];
        List<Pair> translated = [];
        List<Pair> notTranslated = [];

        foreach (var maps in maps.Values) {
            foreach (var map in maps) {
                foreach (var input in inputs) {
                    foreach (var (pair, isTranslated) in map.TranslatePair(input)) {
                        if (isTranslated) {
                            translated.Add(pair);
                        } else {
                            notTranslated.Add(pair);
                        }
                    }
                }

                inputs.Clear();
                inputs.AddRange(notTranslated);
                notTranslated.Clear();
            }

            inputs.AddRange(translated);
            translated.Clear();
        }

        return inputs;
    }
}