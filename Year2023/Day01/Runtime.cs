
using Utilities;

namespace Day01;

readonly struct Runtime {
    readonly string file;

    internal Runtime(string file) {
        this.file = file;
    }

    internal static int FindCalibration(string input) {
        char[] letters = input.ToArray();
        string number = FindDigit(letters) + FindDigit(letters.Reverse());
        return int.Parse(number);
    }

    static string FindDigit(IEnumerable<char> letters) {
        foreach (char letter in letters) {
            if (int.TryParse(letter.ToString(), out int digit)) {
                return digit.ToString();
            }
        }

        throw new Exception();
    }

    internal int calibrationSum => new FileInput(file)
        .ReadLines()
        .Select(FindCalibration)
        .Sum();
}