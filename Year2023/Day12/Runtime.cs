using Utilities;

namespace Day12;

sealed class Runtime {
    internal long sumOfArrangements => records
        .Sum(r => r.numberOfArrangements);

    internal readonly List<Record> records = [];

    internal Runtime(string file, int foldCount = 1) {
        foreach (string row in new FileInput(file).ReadLines()) {
            records.Add(Record.Parse(row, foldCount));
        }
    }
}

sealed record Record(IReadOnlyList<char> springs, IReadOnlyList<int> damagedCounts) {
    internal int numberOfArrangements {
        get {
            char[] overrides = new char[springs.Count];

            return CalculateArrangements(overrides, 0);
        }
    }

    int CalculateArrangements(char[] overrides, int start) {
        int count = 0;
        for (int i = start; i < springs.Count; i++) {
            if (springs[i].IsUnknown()) {
                overrides[i] = '.';
                count += CalculateArrangements(overrides, i + 1);
                overrides[i] = '#';
                count += CalculateArrangements(overrides, i + 1);
                return count;
            }
        }

        if (springs.MatchesCount(damagedCounts, overrides)) {
            count++;
        }

        return count;
    }

    internal static Record Parse(string row, int foldCount = 1) {
        string[] cells = row.Split(' ');
        return new(
            Enumerable.Repeat(cells[0].ToCharArray(), foldCount).SelectMany(i => i).ToArray(),
            Enumerable.Repeat(cells[1].Split(',').Select(int.Parse), foldCount).SelectMany(i => i).ToArray()
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
    internal static bool IsOverride(this char character) {
        return character != default;
    }
    internal static bool MatchesCount(this IReadOnlyList<char> springs, IReadOnlyList<int> damagedCounts, IReadOnlyList<char> overrides) {
        int damagedCount = 0;
        int damagedIndex = 0;
        for (int i = 0; i < springs.Count; i++) {
            char spring = springs[i].IsUnknown()
                ? overrides[i]
                : springs[i];

            if (spring.IsDamaged()) {
                damagedCount++;
            } else {
                if (damagedCount > 0) {
                    if (damagedIndex == damagedCounts.Count || damagedCounts[damagedIndex] != damagedCount) {
                        return false;
                    }

                    damagedIndex++;
                    damagedCount = 0;
                }
            }
        }

        if (damagedCount > 0) {
            if (damagedIndex == damagedCounts.Count || damagedCounts[damagedIndex] != damagedCount) {
                return false;
            }

            damagedIndex++;
            damagedCount = 0;
        }

        return damagedCounts.Count == damagedIndex;
    }
}