using Utilities;

namespace Day15;

sealed class Runtime(string file) {
    internal int sumOfHashes {
        get {
            return hashes.Sum(h => h.code);
        }
    }

    internal long sumOfFocus {
        get {
            int sum = 0;

            foreach (var (boxIndex, box) in boxes) {
                for (int i = 0; i < box.Count; i++) {
                    sum += (boxIndex + 1) * (i + 1) * box[i].focus;
                }
            }

            return sum;
        }
    }

    IEnumerable<Hash> hashes => new FileInput(file)
        .ReadAllText()
        .Split(',')
        .Select(value => value.Trim())
        .Where(value => !string.IsNullOrEmpty(value))
        .Select(value => new Hash(value));

    internal IReadOnlyDictionary<byte, List<Hash>> boxes {
        get {
            var boxes = Enumerable.Range(0, 256)
                .ToDictionary(i => (byte)i, _ => new List<Hash>());

            foreach (var hash in hashes) {
                var box = boxes[hash.labelCode];
                int i = box.FindIndex(h => h.label.Equals(hash.label));
                if (hash.isEqual) {
                    if (i == -1) {
                        box.Add(hash);
                    } else {
                        box[i] = hash;
                    }
                } else {
                    if (i == -1) {
                    } else {
                        box.RemoveAt(i);
                    }
                }
            }

            return boxes;
        }
    }
}

readonly struct Hash {
    internal readonly byte code;
    internal readonly string label;
    internal readonly byte labelCode;
    internal readonly int focus;
    internal readonly bool isEqual;

    public Hash(string text) {
        code = text.GetCode();

        string[] values = text.Split('=');
        if (values.Length == 2) {
            label = values[0];
            isEqual = true;
            focus = int.Parse(values[1]);
        } else {
            label = text[..^1];
        }

        labelCode = label.GetCode();
    }
}

static class Extensions {
    internal static byte GetCode(this string text) {
        byte code = 0;

        for (int i = 0; i < text.Length; i++) {
            // Determine the ASCII code for the current character of the string.
            // Increase the current value by the ASCII code you just determined.
            code += (byte)text[i];

            // Set the current value to itself multiplied by 17.
            code *= 17;

            // Set the current value to the remainder of dividing itself by 256.
            // code %= 256;
        }

        return code;
    }
}