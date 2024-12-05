namespace Utilities;

public readonly struct FileInput(string file, bool trim = true) {
    const string INPUT_FOLDER = "input";

    readonly string path = Path.Combine(INPUT_FOLDER, file);

    public IEnumerable<string> ReadLines() {
        return trim
            ? File.ReadLines(path).Select(line => line.Trim())
            : File.ReadLines(path);
    }

    public string ReadAllText() {
        return File.ReadAllText(path);
    }

    public char[] ReadAllCharacters() {
        return ReadAllText().Trim().ToCharArray();
    }
    public CharacterMap ReadAllAsCharacterMap() {
        return new(ReadAllCharactersAsMap());
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