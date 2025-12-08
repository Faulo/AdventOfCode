using Utilities;

namespace Day08;

sealed partial class Runtime {
    sealed class Circuit {
    }
    readonly List<Vector3Int> boxes = [];
    readonly List<HashSet<Vector3Int>> circuits = [];

    IEnumerable<(Vector3Int left, Vector3Int right)> uniquePairs {
        get {
            for (int i = 0; i < boxes.Count; i++) {
                for (int j = i + 1; j < boxes.Count; j++) {
                    yield return (boxes[i], boxes[j]);
                }
            }
        }
    }

    internal Runtime(string file, int connectionCount) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] coordinates = line.Split(',');
            if (int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y) && int.TryParse(coordinates[2], out int z)) {
                var position = new Vector3Int(x, y, z);
                boxes.Add(position);
                circuits.Add([position]);
            }
        }

        var boxPairs = uniquePairs
            .OrderBy(pair => Vector3Int.DistanceSquared(pair.left, pair.right))
            .Take(connectionCount);

        foreach (var (left, right) in boxPairs) {
            MergeCircuits(left, right);
        }
    }

    void MergeCircuits(Vector3Int left, Vector3Int right) {
        var leftCircuit = FindCircuit(left);
        var rightCircuit = FindCircuit(right);
        if (leftCircuit == rightCircuit) {
            return;
        }

        leftCircuit.UnionWith(rightCircuit);
        circuits.Remove(rightCircuit);
    }

    HashSet<Vector3Int> FindCircuit(Vector3Int box) => circuits.First(c => c.Contains(box));

    internal int circuitCount => circuits.Count;
    internal int circuitAggregate => circuits
        .OrderByDescending(c => c.Count)
        .Take(3)
        .Select(c => c.Count)
        .Aggregate(1, (a, b) => a * b);

    internal long allFreshIngredients => 0;
}