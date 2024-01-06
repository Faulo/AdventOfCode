using NUnit.Framework;
using Utilities;

namespace Day24;

sealed class Runtime {
    /// <summary>
    /// <seealso href="https://github.com/jmd-dk/advent-of-code/blob/main/2023/solution/24/solve.py"/>
    /// </summary>
    /// <param name="h1"></param>
    /// <param name="h2"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    internal static bool TryCalculateIntersection(Hailstone h1, Hailstone h2, out (double x, double y) position) {
        long dx = h1.position.x - h2.position.x;
        long dy = h1.position.y - h2.position.y;

        long denominator = (h1.velocity.y * h2.velocity.x) - (h1.velocity.x * h2.velocity.y);
        if (denominator == 0) {
            position = default;
            return false;
        }
        long numerator1 = (h2.velocity.y * dx) - (h2.velocity.x * dy);
        double t1 = (double)numerator1 / denominator;
        if (t1 <= 0) {
            position = default;
            return false;
        }

        long numerator2 = (h1.velocity.y * dx) - (h1.velocity.x * dy);
        double t2 = (double)numerator2 / denominator;
        if (t2 <= 0) {
            position = default;
            return false;
        }

        double x = h1.position.x + (t1 * h1.velocity.x);
        double y = h1.position.y + (t1 * h1.velocity.y);

        position = (x, y);

        var delta1 = position;
        delta1.x -= h1.position.x;
        delta1.y -= h1.position.y;

        var delta2 = position;
        delta2.x -= h2.position.x;
        delta2.y -= h2.position.y;

        // Console.WriteLine($"{Math.Sign(h1.velocity.x) == Math.Sign(delta1.x)} && {Math.Sign(h1.velocity.y) == Math.Sign(delta1.y)} && {Math.Sign(h2.velocity.x) == Math.Sign(delta2.x)} && {Math.Sign(h2.velocity.y) == Math.Sign(delta2.y)}");

        return Math.Sign(h1.velocity.x) == Math.Sign(delta1.x)
            && Math.Sign(h1.velocity.y) == Math.Sign(delta1.y)
            && Math.Sign(h2.velocity.x) == Math.Sign(delta2.x)
            && Math.Sign(h2.velocity.y) == Math.Sign(delta2.y);
    }

    /// <summary>
    /// <seealso href="https://stackoverflow.com/questions/4543506/algorithm-for-intersection-of-2-lines"/>
    /// </summary>
    /// <param name="h1"></param>
    /// <param name="h2"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    internal static bool TryCalculateIntersectionWrong(Hailstone h1, Hailstone h2, out (long x, long y) position) {
        long determinant = (h1.line.x * h2.line.y) - (h2.line.x * h1.line.y);

        if (determinant == 0) {
            position = default;
            return false;
        }

        long x = ((h2.line.y * h1.line.z) - (h1.line.y * h2.line.z)) / determinant;
        long y = ((h1.line.x * h2.line.z) - (h2.line.x * h1.line.z)) / determinant;
        position = (x, y);

        var delta1 = position;
        delta1.x -= h1.position.x;
        delta1.y -= h1.position.y;

        var delta2 = position;
        delta2.x -= h2.position.x;
        delta2.y -= h2.position.y;

        // Console.WriteLine($"{Math.Sign(h1.velocity.x) == Math.Sign(delta1.x)} && {Math.Sign(h1.velocity.y) == Math.Sign(delta1.y)} && {Math.Sign(h2.velocity.x) == Math.Sign(delta2.x)} && {Math.Sign(h2.velocity.y) == Math.Sign(delta2.y)}");

        return Math.Sign(h1.velocity.x) == Math.Sign(delta1.x)
            && Math.Sign(h1.velocity.y) == Math.Sign(delta1.y)
            && Math.Sign(h2.velocity.x) == Math.Sign(delta2.x)
            && Math.Sign(h2.velocity.y) == Math.Sign(delta2.y);
    }

    internal readonly List<Hailstone> hailstones = [];

    internal int GetNumberOfCollisions(long min, long max) {
        int count = 0;
        for (int i = 0; i < hailstones.Count; i++) {
            for (int j = i + 1; j < hailstones.Count; j++) {
                var h1 = hailstones[i];
                var h2 = hailstones[j];
                if (TryCalculateIntersection(h1, h2, out var position)) {
                    if (position.x >= min && position.y >= min && position.x <= max && position.y <= max) {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            hailstones.Add(Hailstone.Parse(line));
        }
    }
}

sealed class Hailstone {
    internal readonly Vector3Int position;
    internal readonly Vector3Int velocity;
    internal readonly Vector3Int line;

    internal Hailstone(Vector3Int position, Vector3Int velocity) {
        Assert.That(velocity.x, Is.Not.EqualTo(0));
        Assert.That(velocity.y, Is.Not.EqualTo(0));

        this.position = position;
        this.velocity = velocity;

        var p1 = position;
        var p2 = position + velocity;
        line.x = p2.y - p1.y;
        line.y = p1.x - p2.x;
        line.z = (line.x * p1.x) + (line.y * p1.y);
        //Assert.That(line.z, Is.EqualTo(((double)line.x * p1.x) + ((double)line.y * p1.y)), $"{line.x} * {p1.x} + {line.y} * {p1.y}");
    }

    public override string ToString() => line.ToString();

    internal static Hailstone Parse(string line) {
        var positions = line
            .Replace('@', ',')
            .Split(',')
            .Select(long.Parse)
            .ToList();

        return new(new(positions[0], positions[1], positions[2]), new(positions[3], positions[4], positions[5]));
    }
}

static class Extensions {
}