using Utilities;

namespace Day07;

sealed partial class Runtime {
    readonly struct Row {
        internal readonly long result;
        readonly long[] operands;

        internal Row(long result, long[] operands) {
            this.result = result;
            this.operands = operands;
        }

        internal bool canBeTrue {
            get {
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
        }

        internal bool canBeThree {
            get {
                int length = (int)Math.Pow(4, operands.Length - 1);

                return Enumerable
                    .Range(0, length)
                    .Any(CanBeThreeInternal);
            }
        }

        bool CanBeThreeInternal(int i) {
            long test = operands[0];

            for (int j = 1; j < operands.Length; j++) {
                int op = (i >> (2 * (j - 1))) & 3;

                switch (op) {
                    case 0:
                        test *= operands[j];
                        break;
                    case 1:
                        test += operands[j];
                        break;
                    case 2:
                        test = long.Parse(test.ToString() + operands[j]);
                        break;
                    default:
                        return false;
                }
            }

            return test == result;
        }
    }

    readonly string file;

    ParallelQuery<Row> rows => new FileInput(file)
        .ReadLines()
        .AsParallel()
        .Select(row => row.Split(": "))
        .Select(row => new Row(long.Parse(row[0]), row[1].Split(' ').Select(long.Parse).ToArray()));

    internal Runtime(string file) {
        this.file = file;
    }

    internal long sumOfTrue {
        get {
            return rows
                .Where(row => row.canBeTrue)
                .Sum(row => row.result);
        }
    }

    internal long sumOfThree {
        get {
            return rows
                .Where(row => row.canBeThree)
                .Sum(row => row.result);
        }
    }

    internal static bool CanBeTrue(long result, long[] operands) => new Row(result, operands).canBeTrue;

    internal static bool CanBeThree(long result, long[] operands) => new Row(result, operands).canBeThree;
}