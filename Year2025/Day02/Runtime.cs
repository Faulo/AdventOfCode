using Utilities;

namespace Day02;

sealed partial class Runtime {
    internal readonly List<(long first, long last)> ranges = [];

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            foreach (string range in line.Split(',')) {
                string[] firstlast = range.Split('-');
                long first = long.Parse(firstlast[0]);
                long last = long.Parse(firstlast[1]);
                ranges.Add((first, last));
            }
        }
    }

    internal long sumOfInvalidIds => ranges
        .SelectMany(range => FindInvalidIds(range.first, range.last))
        .Sum();

    internal static IEnumerable<long> FindInvalidIds(long first, long last) {
        for (long i = first; i <= last; i++) {
            int rank = Convert.ToInt32(Math.Ceiling(Math.Log10(i)));
            if (rank % 2 == 0) {
                long multiplier = Convert.ToInt64(Math.Pow(10, rank / 2));
                long left = i / multiplier;
                long right = i - (left * multiplier);
                if (left == right) {
                    yield return i;
                }
            }
        }
    }
}