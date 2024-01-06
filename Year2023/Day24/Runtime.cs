using Utilities;

namespace Day24;

sealed class Runtime {
    internal readonly List<Hailstone> hailstones = [];

    internal int GetNumberOfCollisions(long min, long max) {
        return hailstones.Count;
    }

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            hailstones.Add(Hailstone.Parse(line));
        }
    }
}

sealed class Hailstone {
    internal Vector3Int position { get; private set; }
    internal Vector3Int velocity { get; private set; }

    internal static Hailstone Parse(string line) {
        var positions = line
            .Replace('@', ',')
            .Split(',')
            .Select(long.Parse)
            .ToList();

        return new() {
            position = new(positions[0], positions[1], positions[2]),
            velocity = new(positions[3], positions[4], positions[5]),
        };
    }
}

static class Extensions {
}