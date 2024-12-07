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
                int bit = 1 << (j - 1);
                bool isMult = (i & bit) == bit;
                test = isMult
                   ? test * operands[j]
                   : test + operands[j];
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
                .Where(row => CanBeTrue(row.result, row.operands))
                .Sum(r => r.result);
        }
    }
}