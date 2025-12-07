using System.Text.RegularExpressions;
using Utilities;

namespace Day06;

sealed partial class Runtime {
    enum EOperation {
        Add,
        Multiply
    }
    readonly List<List<long>> numberLists = [];
    readonly List<EOperation> operations = [];

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] numbers = Whitespace().Split(line);
            if (long.TryParse(numbers[0], out _)) {
                for (int j = 0; j < numbers.Length; j++) {
                    if (j == numberLists.Count) {
                        numberLists.Add([]);
                    }

                    numberLists[j].Add(long.Parse(numbers[j]));
                }
            } else {
                operations = [.. numbers.Select(ParseOperation)];
            }
        }
    }

    static EOperation ParseOperation(string value) => value switch {
        "+" => EOperation.Add,
        "*" => EOperation.Multiply,
        _ => throw new NotImplementedException(),
    };

    internal long grandTotal {
        get {
            long sum = 0;
            for (int i = 0; i < operations.Count; i++) {
                var numberList = numberLists[i];
                sum += operations[i] switch {
                    EOperation.Add => numberList.Sum(),
                    EOperation.Multiply => numberList.Aggregate((long)1, (a, b) => a * b),
                    _ => throw new NotImplementedException(),
                };
            }

            return sum;
        }
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex Whitespace();
}