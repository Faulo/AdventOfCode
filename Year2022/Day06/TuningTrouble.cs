using Utilities;

namespace Day06;

class TuningTrouble {
    readonly FileInput input;

    internal TuningTrouble(string file) {
        input = new(file);
    }

    internal char[] ReadFile() {
        return input.ReadAllCharacters();
    }

    internal int FindStartInFile(MessageType type) {
        return FindStart(ReadFile(), type);
    }

    internal static int FindStart(char[] data, MessageType type) {
        var dict = new Dictionary<char, short>();
        int length = type switch {
            MessageType.StartOfPacket => 4,
            MessageType.StartOfMessage => 14,
            _ => throw new NotImplementedException(),
        };
        for (int i = 0; i < data.Length; i++) {
            char current = data[i];
            if (dict.ContainsKey(current)) {
                dict[current]++;
            } else {
                dict[current] = 1;
            }

            if (i >= length) {
                char previous = data[i - length];
                if (dict[previous] == 1) {
                    dict.Remove(previous);
                } else {
                    dict[previous]--;
                }
            }

            if (dict.Count == length) {
                return i + 1;
            }
        }

        throw new Exception();
    }
}
