using Utilities;

namespace Day07;

sealed partial class Runtime {

    internal readonly (long result, long[] operands)[] rows;

    internal Runtime(string file) {
        rows = new FileInput(file)
            .ReadLines()
            .Select(row => row.Split(": "))
            .Select(row => (long.Parse(row[0]), row[1].Split(' ').Select(long.Parse).ToArray()))
            .ToArray();
    }

    internal static bool CanBeTrue(long result, long[] operands) {
        int length = (int)Math.Pow(2, operands.Length - 1);

        for (int i = 0; i < length; i++) {
            long test = operands[0];

            for (int j = 1; j < operands.Length; j++) {
                test = ((i >> (j - 1)) & 1) switch {
                    0 => test * operands[j],
                    1 => test + operands[j],
                    _ => 0,
                };

                if (test == 0) {
                    return false;
                }
            }

            if (test == result) {
                return true;
            }
        }

        return false;
    }

    internal static bool CanBeThree(long result, long[] operands) {
        int length = (int)Math.Pow(4, operands.Length - 1);

        for (int i = 0; i < length; i++) {
            long test = operands[0];

            for (int j = 1; j < operands.Length; j++) {
                int op = (i >> (2 * (j - 1))) & 3;
                test = op switch {
                    0 => test * operands[j],
                    1 => test + operands[j],
                    2 => long.Parse(test.ToString() + operands[j]),
                    3 => 0,
                    _ => throw new NotImplementedException(),
                };

                if (test == 0) {
                    break;
                }
            }

            if (test == result) {
                return true;
            }
        }

        return false;
    }

    internal long sumOfTrue {
        get {
            return rows
                .AsParallel()
                .Where(row => CanBeTrue(row.result, row.operands))
                .Sum(r => r.result);
        }
    }

    internal long sumOfThree {
        get {
            return rows
                .AsParallel()
                .Where(row => CanBeThree(row.result, row.operands))
                .Sum(r => r.result);
        }
    }
}