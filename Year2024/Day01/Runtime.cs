using Utilities;

namespace Day01;

sealed class Runtime {
    internal readonly List<int> left = [];
    internal readonly List<int> right = [];

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] numbers = line.Split(' ');
        }
    }

    internal static int Delta(int left, int right) => Math.Abs(left - right);

    internal int totalDistance => 0;
}