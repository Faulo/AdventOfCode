namespace Utilities;

public readonly struct FileInput {
    const string INPUT_FOLDER = "input";

    readonly string path;

    public FileInput(string file) {
        path = Path.Combine(INPUT_FOLDER, file);
    }

    public IEnumerable<string> ReadLines() {
        return File.ReadLines(path).Select(line => line.Trim());
    }

    public string ReadAllText() {
        return File.ReadAllText(path);
    }

    public char[] ReadAllCharacters() {
        return ReadAllText().Trim().ToCharArray();
    }

    public char[,] ReadAllCharactersAsMap() {
        int width = 0;
        int height = 0;

        foreach (string line in ReadLines()) {
            width = line.Length;
            height++;
        }

        char[,] map = new char[width, height];

        int y = 0;
        foreach (string line in ReadLines()) {
            int x = 0;
            foreach (char c in line.ToCharArray()) {
                map[x, y] = c;
                x++;
            }

            y++;
        }

        return map;
    }
}