using Utilities;

namespace Day15;

sealed class Runtime(string file) {
    internal long sumOfHashes {
        get {
            long sum = 0;

            Parallel.ForEach(hashes, map => Interlocked.Add(ref sum, map.GetHashCode()));

            return sum;
        }
    }

    IEnumerable<Hash> hashes => new FileInput(file)
        .ReadAllText()
        .Split(',')
        .Select(value => value.Trim())
        .Where(value => !string.IsNullOrEmpty(value))
        .Select(value => new Hash(value));
}

readonly struct Hash(string text) {
    public override int GetHashCode() {
        int value = 0;

        for (int i = 0; i < text.Length; i++) {
            // Determine the ASCII code for the current character of the string.
            // Increase the current value by the ASCII code you just determined.
            value += text[i];

            // Set the current value to itself multiplied by 17.
            value *= 17;

            // Set the current value to the remainder of dividing itself by 256.
            value %= 256;
        }

        return value;
    }
}

static class Extensions {
}