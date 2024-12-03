using Utilities;

namespace Day01;

sealed class Runtime {
    readonly string file;
    readonly bool findWords;

    internal Runtime(string file, bool findWords = false) {
        this.file = file;
        this.findWords = findWords;
    }

    internal static int FindCalibration(string input, bool findWords = false) {
        var indices = Enumerable.Range(0, input.Length);
        string number = FindDigit(input, indices, findWords) + FindDigit(input, indices.Reverse(), findWords);
        return int.Parse(number);
    }

    static string FindDigit(string letters, IEnumerable<int> indices, bool findWords) {
        foreach (int i in indices) {
            if (TryParse(letters, i, out int digit, findWords)) {
                return digit.ToString();
            }
        }

        throw new Exception();
    }

    static readonly string[] words = [
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
    ];

    static bool TryParse(string letters, int index, out int digit, bool findWords) {
        if (findWords) {
            for (int i = 0; i < words.Length; i++) {
                if (TryFind(letters, index, words[i])) {
                    digit = i + 1;
                    return true;
                }
            }
        }

        return int.TryParse(letters[index].ToString(), out digit);
    }

    static bool TryFind(string letters, int i, string word) {
        return letters.Length > i && letters[i..].StartsWith(word);
    }

    internal int calibrationSum => new FileInput(file)
        .ReadLines()
        .Select(value => FindCalibration(value, findWords))
        .Sum();
}