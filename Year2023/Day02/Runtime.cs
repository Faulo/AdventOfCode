using System.Text.RegularExpressions;
using Utilities;

namespace Day02;

sealed class Runtime {
    internal class Game {

    }

    readonly Dictionary<string, int> cubes = [];
    internal Runtime() {
        AddCubes("red", 12);
        AddCubes("green", 13);
        AddCubes("blue", 14);
    }

    void AddCubes(string color, int amount) {
        cubes[color] = amount;
    }

    internal int CalculatePossible(string file) {
        int sum = 0;
        foreach (string line in new FileInput(file).ReadLines()) {
            var game = ParseLine(line);
            bool isValid = cubes
                .All(cube => game.cubes.TryGetValue(cube.Key, out int amount) && cube.Value >= amount);

            if (isValid) {
                sum += game.id;
            }
        }

        return sum;
    }

    internal int CalculatePower(string file) {
        int sum = 0;
        foreach (string line in new FileInput(file).ReadLines()) {
            var game = ParseLine(line);
            sum += game.cubes.Values.Aggregate(1, (i, j) => i * j);
        }

        return sum;
    }

    internal static (int id, IReadOnlyDictionary<string, int> cubes) ParseLine(string line) {
        var match = Regex.Match(line, "Game (\\d+):");
        int id = int.Parse(match.Groups[1].Value);

        var cubes = new Dictionary<string, int>();

        var matches = Regex.Matches(line, "(\\d+) ([a-z]+)");
        foreach (Match m in matches) {
            int amount = int.Parse(m.Groups[1].Value);
            string color = m.Groups[2].Value;

            if (cubes.TryGetValue(color, out int previous) && previous > amount) {
                continue;
            }

            cubes[color] = amount;
        }

        return (id, cubes);
    }
}