using Utilities;

namespace Day12;

sealed class Runtime {
    internal long sumOfArrangements {
        get {
            long sum = 0;

            Parallel.ForEach(records, record => Interlocked.Add(ref sum, record.numberOfArrangements));

            return sum;
        }
    }

    internal readonly List<Record> records = [];

    internal Runtime(string file, int foldCount = 1) {
        foreach (string row in new FileInput(file).ReadLines()) {
            records.Add(Record.Parse(row, foldCount));
        }
    }
}

sealed record Record(IReadOnlyList<char> springs, IReadOnlyList<int> damagedCounts) {
    char this[int i] => i < springs.Count
        ? springs[i]
        : '.';

    internal long numberOfArrangements {
        get {
            return CountDamageCounts(0, 0);
        }
    }

    long CountDamageCounts(int damagedIndex, int start) {
        if (damagedIndex == damagedCounts.Count) {
            for (int i = start; i < springs.Count; i++) {
                if (springs[i].IsDamaged()) {
                    return 0;
                }
            }

            return 1;
        }

        long count = 0;
        foreach (int index in FindDamagedCount(damagedCounts[damagedIndex], start)) {
            count += CountDamageCounts(damagedIndex + 1, index);
        }

        return count;
    }

    internal IEnumerable<int> FindDamagedCount(int damagedCount, int start) {
        for (; start < springs.Count && springs[start].IsOperational(); start++) {
        }

        while (start < springs.Count) {
            bool found = true;
            for (int i = 0; i < damagedCount; i++) {
                if (!this[start + i].CouldBeDamaged()) {
                    found = false;
                    break;
                }
            }

            if (found && this[start + damagedCount].CouldBeOperational()) {
                yield return start + damagedCount + 1;
            }

            if (this[start].IsDamaged()) {
                break;
            }

            start++;
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
            string.Join('?', Enumerable.Repeat(cells[0], foldCount)).ToCharArray(),
            string.Join(',', Enumerable.Repeat(cells[1], foldCount)).Split(',').Select(int.Parse).ToArray()
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
        return character is '.';
    }
    internal static bool CouldBeOperational(this char character) {
        return character is '.' or '?';
    }
    internal static bool IsDamaged(this char character) {
        return character is '#';
    }
    internal static bool CouldBeDamaged(this char character) {
        return character is '#' or '?';
    }
    internal static bool IsUnknown(this char character) {
        return character is '?';
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