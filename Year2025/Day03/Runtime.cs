using Utilities;

namespace Day03;

sealed partial class Runtime {
    internal readonly List<string> banks = [];
    internal readonly int count;

    internal Runtime(string file, int count = 2) {
        banks.AddRange(new FileInput(file).ReadLines());
        this.count = count;
    }

    internal long totalJoltage => banks.Sum(b => FindHighestJoltage(b, count));

    const bool USE_STACK = true;

    internal static long FindHighestJoltage(string bank, int count = 2) {
        Span<char> digits = USE_STACK ? stackalloc char[count] : new char[count];
        int start = 0;
        for (int c = 0; c < count; c++) {
            digits[c] = '0';
            int end = bank.Length - count + c + 1;
            for (int i = start; i < end; i++) {
                if (bank[i] > digits[c]) {
                    digits[c] = bank[i];
                    start = i + 1;
                }
            }
        }

        return long.Parse(digits);
    }
}