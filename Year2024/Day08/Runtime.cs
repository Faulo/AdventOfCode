using Utilities;

namespace Day08;

sealed partial class Runtime {

    readonly struct Antenna(CharacterMap map, char type) {
        readonly Vector2Int[] positions = map
            .allPositionsAndCharactersWithin
            .Where(tile => tile.character == type)
            .Select(tile => tile.position)
            .ToArray();

        internal void FindAntinodes(HashSet<Vector2Int> antinodes, bool anyDistance = false) {
            foreach (var a in positions) {
                foreach (var b in positions) {
                    if (a == b) {
                        continue;
                    }

                    var delta = a - b;

                    if (anyDistance) {
                        for (int i = 0; map.IsInBounds(a + (delta * i)); i++) {
                            antinodes.Add(a + (delta * i));
                        }
                    } else {
                        var c = a + delta;
                        if (map.IsInBounds(c)) {
                            antinodes.Add(c);
                        }
                    }
                }
            }
        }
    }

    readonly CharacterMap map;
    readonly Dictionary<char, Antenna> antennas = [];

    internal Runtime(string file) {
        map = new FileInput(file)
            .ReadAllAsCharacterMap();

        foreach (var (position, character) in map.allPositionsAndCharactersWithin) {
            if (character == '.') {
                continue;
            }

            if (!antennas.ContainsKey(character)) {
                antennas[character] = new Antenna(map, character);
            }
        }
    }

    internal long simpleAntinodeCount {
        get {
            var positions = new HashSet<Vector2Int>();

            foreach (var a in antennas.Values) {
                a.FindAntinodes(positions);
            }

            return positions.Count;
        }
    }

    internal long complexAntinodeCount {
        get {
            var positions = new HashSet<Vector2Int>();

            foreach (var a in antennas.Values) {
                a.FindAntinodes(positions, true);
            }

            return positions.Count;
        }
    }
}