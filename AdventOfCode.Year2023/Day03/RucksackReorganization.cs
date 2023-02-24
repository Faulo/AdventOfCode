namespace Day03;

class RucksackReorganization {
    const string INPUT_FOLDER = "input";

    public static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    public static (string, string) DivideIntoCompartments(string line) {
        return (line[..(line.Length / 2)], line[(line.Length / 2)..]);
    }

    public static char FindLetterThatAppearsInBothCompartments(string line) {
        var (first, second) = DivideIntoCompartments(line);

        for (int i = 0; i < first.Length; i++) {
            char letter = first[i];
            for (int j = 0; j < second.Length; j++) {
                if (letter == second[j]) {
                    return letter;
                }
            }
        }

        throw new Exception("No letter appeared in both!");
    }
}
