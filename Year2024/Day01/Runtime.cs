using System.Text.RegularExpressions;
using Utilities;

namespace Day01;

sealed class Runtime {
    internal readonly List<int> left = [];
    internal readonly List<int> right = [];

    readonly Regex numberExpression = new("\\d+");

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            var matches = numberExpression.Matches(line);
            left.Add(int.Parse(matches[0].Value));
            right.Add(int.Parse(matches[1].Value));
        }
    }

    internal static int Delta(int left, int right) => Math.Abs(left - right);

    internal int totalDistance {
        get {
            int sum = 0;

            while (left.Count > 0) {
                int leftMin = left.Min();
                left.Remove(leftMin);
                int rightMin = right.Min();
                right.Remove(rightMin);

                sum += Delta(leftMin, rightMin);
            }

            return sum;
        }
    }
}