﻿using System.Text.RegularExpressions;
using Utilities;

namespace Day01;

sealed partial class Runtime {
    internal readonly int[][] map;
    readonly bool useDampener;

    [GeneratedRegex("\\d+", RegexOptions.Compiled)]
    private static partial Regex NumberExpression();

    internal Runtime(string file, bool useDampener = false) {
        map = new FileInput(file)
            .ReadLines()
            .Select(line => NumberExpression().Matches(line).Select(m => int.Parse(m.Value)).ToArray())
            .ToArray();
        this.useDampener = useDampener;
    }

    internal static int SafeScore(int left, int right) {
        return (right - left) switch {
            < -3 or 0 or > 3 => 0,
            > 0 => 1,
            < 0 => -1,
        };
    }

    bool IsSafe(int[] row) {
        if (IsSafe(new(row), -1)) {
            return true;
        }

        if (useDampener) {
            for (int skipIndex = 0; skipIndex < row.Length; skipIndex++) {
                if (IsSafe(new(row), skipIndex)) {
                    return true;
                }
            }
        }

        return false;
    }

    bool IsSafe(List<int> row, int skipIndex) {
        if (skipIndex != -1) {
            row.RemoveAt(skipIndex);
        }

        int count = 0;
        for (int i = 1; i < row.Count; i++) {
            count += SafeScore(row[i - 1], row[i]);
        }

        count = Math.Abs(count);

        return count + 1 == row.Count;
    }

    internal int safeReports {
        get {
            return map.Count(IsSafe);
        }
    }
}