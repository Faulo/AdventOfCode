namespace Day01;

class Program {
    const string INPUT_FOLDER = "input";

    public static int CountCalories(string file) {
        return file.Length;
    }

    public static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }
}