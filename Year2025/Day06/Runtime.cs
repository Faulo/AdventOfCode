using System.Text.RegularExpressions;
using Utilities;

namespace Day06;

sealed partial class Runtime {
    class Problem {
        readonly CharacterMap map;
        readonly int x;
        readonly int xmax;
        int width => xmax - x;
        readonly int y;
        readonly int ymax;
        int height => ymax - y;
        readonly EOperation operation;
        readonly bool parseHorizontally;

        public Problem(CharacterMap map, int x, int xmax, bool leftToRight) {
            this.map = map;
            this.x = x;
            this.xmax = xmax;
            parseHorizontally = leftToRight;

            y = 0;
            ymax = map.height - 1;
            operation = ParseOperation(map[x, ymax]);
        }

        IEnumerable<long> numbers {
            get {
                if (parseHorizontally) {
                    char[] builder = new char[width];
                    for (int y = this.y; y < ymax; y++) {
                        for (int x = this.x; x < xmax; x++) {
                            builder[x - this.x] = map[x, y];
                        }

                        if (long.TryParse(builder, out long value)) {
                            yield return value;
                        }
                    }
                } else {
                    char[] builder = new char[height];
                    for (int x = this.x; x < xmax; x++) {
                        for (int y = this.y; y < ymax; y++) {
                            builder[y - this.y] = map[x, y];
                        }

                        if (long.TryParse(builder, out long value)) {
                            yield return value;
                        }
                    }
                }
            }
        }

        internal long total => operation switch {
            EOperation.Add => numbers.Sum(),
            EOperation.Multiply => numbers.Aggregate((long)1, (a, b) => a * b),
            _ => throw new NotImplementedException(),
        };
    }
    enum EOperation {
        Add,
        Multiply
    }
    readonly List<Problem> problems = [];

    internal Runtime(string file, bool parseHorizontally = true) {
        var input = new FileInput(file, false);
        var map = input.ReadAllAsCharacterMap();
        string operations = input.ReadLines().Last();

        int previous = 0;
        for (int x = 1; x <= operations.Length; x++) {
            if (x == operations.Length || operations[x] != ' ') {
                problems.Add(new(map, previous, x, parseHorizontally));
                previous = x;
            }
        }
    }

    static EOperation ParseOperation(char value) => value switch {
        '+' => EOperation.Add,
        '*' => EOperation.Multiply,
        _ => throw new NotImplementedException(),
    };

    internal long grandTotal => problems.Sum(p => p.total);

    [GeneratedRegex("\\s+")]
    private static partial Regex Whitespace();
}