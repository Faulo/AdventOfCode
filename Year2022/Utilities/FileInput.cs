namespace Utilities;

public readonly struct FileInput {
    const string INPUT_FOLDER = "input";

    readonly string path;

    public FileInput(string file) {
        path = Path.Combine(INPUT_FOLDER, file);
    }

    public IEnumerable<string> ReadLines() {
        return File.ReadLines(path);
    }

    public string ReadAllText() {
        return File.ReadAllText(path);
    }

    public char[] ReadAllCharacters() {
        return ReadAllText().Trim().ToCharArray();
    }
}