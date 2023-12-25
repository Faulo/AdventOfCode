using Utilities;

namespace Day12;

sealed class Runtime {
    internal long sumOfArrangements => 0;

    internal readonly List<Record> records = [];

    internal Runtime(string file) {
        foreach (string row in new FileInput(file).ReadLines()) {
            records.Add(Record.Parse(row));
        }
    }
}

sealed record Record(char[] springs, int[] damagedCounts) {
    internal static Record Parse(string row) {
        string[] cells = row.Split(' ');
        return new(
            cells[0].ToCharArray(),
            cells[1].Split(',').Select(int.Parse).ToArray()
        );
    }

    public bool Equals(Record? other) {
        return other is not null
            && springs.SequenceEqual(other.springs)
            && damagedCounts.SequenceEqual(other.damagedCounts);
    }

    public override int GetHashCode() => HashCode.Combine(springs, damagedCounts);
}

static class Extensions {
    internal static bool IsOperational(this char character) {
        return character == '.';
    }
    internal static bool IsDamaged(this char character) {
        return character == '#';
    }
    internal static bool IsUnknown(this char character) {
        return character == '?';
    }
}