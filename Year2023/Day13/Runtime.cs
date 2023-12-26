using Utilities;

namespace Day13;

sealed class Runtime {
    static readonly bool useMultithreading = true;

    internal int sumOfReflections {
        get {
            if (useMultithreading) {
                int sum = 0;

                Parallel.ForEach(maps, map => Interlocked.Add(ref sum, map.reflection));

                return sum;
            } else {
                return maps.Sum(map => map.reflection);
            }
        }
    }

    internal readonly IReadOnlyList<Map> maps;

    internal Runtime(string file) {
        var maps = new List<Map>();
        var rows = new List<string>();
        foreach (string row in new FileInput(file).ReadLines()) {
            if (string.IsNullOrWhiteSpace(row)) {
                if (rows.Count > 0) {
                    maps.Add(new(rows));
                    rows = [];
                }
            } else {
                rows.Add(row);
            }
        }

        if (rows.Count > 0) {
            maps.Add(new(rows));
        }

        this.maps = maps;
    }
}

sealed record Map {
    internal int reflection {
        get {
            if (rows.TryFindReflection(out int row)) {
                return 100 * row;
            }

            if (columns.TryFindReflection(out int column)) {
                return column;
            }

            throw new Exception();
        }
    }

    internal readonly IReadOnlyList<string> rows;
    internal readonly IReadOnlyList<string> columns;

    internal Map(IReadOnlyList<string> rows) {
        this.rows = rows;

        columns = Enumerable
            .Range(0, rows[0].Length)
            .Select(c => new string(rows.Select(r => r[c]).ToArray()))
            .ToArray();
    }

    public bool Equals(Map? other) {
        return other is not null
            && rows.SequenceEqual(other.rows);
    }

    public override int GetHashCode() => HashCode.Combine(rows);
}

static class Extensions {
    internal static bool TryFindReflection(this IReadOnlyList<string> rows, out int i) {
        for (i = 1; i < rows.Count; i++) {
            int extends = Math.Min(i, rows.Count - i);
            bool isReflection = true;
            for (int j = 0; j < extends; j++) {
                if (rows[i - 1 - j] != rows[i + j]) {
                    isReflection = false;
                    break;
                }
            }

            if (isReflection) {
                return true;
            }
        }

        return false;
    }
}