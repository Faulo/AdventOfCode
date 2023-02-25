namespace Day01;

class CalorieCount {
    const string INPUT_FOLDER = "input";

    internal static int FindMostCalories(string file) {
        return ReadFileToElf(file)
            .Max();
    }

    internal static int FindSumOfTopThreeCalories(string file) {
        return ReadFileToElf(file)
            .OrderByDescending(v => v)
            .Take(3)
            .Sum();
    }

    internal static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    internal static IEnumerable<int> ReadFileToElf(string file) {
        int elf = 0;
        foreach (string line in ReadFileToArray(file)) {
            if (string.IsNullOrEmpty(line)) {
                if (elf > 0) {
                    yield return elf;
                }
                elf = 0;
            } else {
                elf += int.Parse(line);
            }
        }
        if (elf > 0) {
            yield return elf;
        }
    }
}