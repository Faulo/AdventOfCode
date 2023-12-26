using Utilities;

namespace Day13;

sealed class Runtime {
    internal int sumOfReflections {
        get {
            int sum = 0;

            Parallel.ForEach(maps, map => Interlocked.Add(ref sum, map.reflection));

            return sum;
        }
    }
    internal int sumOfReflectionsWithSmudgesFixed {
        get {
            int sum = 0;

            Parallel.ForEach(maps, map => Interlocked.Add(ref sum, map.reflectionWithSmudgesFixed));

            return sum;
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
            if (columns.TryFindReflection(out int column)) {
                return column;
            }

            if (rows.TryFindReflection(out int row)) {
                return 100 * row;
            }

            throw new Exception();
        }
    }

    internal int reflectionWithSmudgesFixed {
        get {
            if (columns.TryFindReflectionWidthSmudges(out int column)) {
                return column;
            }

            if (rows.TryFindReflectionWidthSmudges(out int row)) {
                return 100 * row;
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

    internal static bool TryFindReflectionWidthSmudges(this IReadOnlyList<string> rows, out int i) {
        for (i = 1; i < rows.Count; i++) {
            int extends = Math.Min(i, rows.Count - i);
            bool isReflection = true;
            bool smudgeFound = false;
            for (int j = 0; j < extends; j++) {
                var similarity = AreSimilar(rows[i - 1 - j], rows[i + j]);

                if (similarity == Similarity.NotEqual) {
                    isReflection = false;
                    break;
                }

                if (similarity == Similarity.WidthSmudge) {
                    if (smudgeFound) {
                        isReflection = false;
                        break;
                    }

                    smudgeFound = true;
                }
            }

            if (isReflection && smudgeFound) {
                return true;
            }
        }

        return false;
    }

    enum Similarity {
        Equal,
        WidthSmudge,
        NotEqual
    }
    static Similarity AreSimilar(string a, string b) {
        bool smudgeFound = false;
        for (int i = 0; i < a.Length; i++) {
            if (a[i] != b[i]) {
                if (smudgeFound) {
                    return Similarity.NotEqual;
                }

                smudgeFound = true;
            }
        }

        return smudgeFound
            ? Similarity.WidthSmudge
            : Similarity.Equal;
    }
}