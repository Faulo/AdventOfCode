using Utilities;

namespace Day07;

sealed partial class Runtime {
    sealed class Splitter {
        internal readonly Vector2Int position;

        internal readonly List<Splitter> ancestors = [];

        public Splitter(Vector2Int position) => this.position = position;

        internal bool isSplitting {
            get => _isSplitting ??= ancestors.Any(s => s.isSplitting);
            set => _isSplitting = value;
        }

        bool? _isSplitting = null;
    }

    const char START = 'S';
    const char SPLITTER = '^';

    internal Runtime(string file) {
        var map = new FileInput(file).ReadAllAsCharacterMap();
        var start = map
            .allPositionsAndCharactersWithin
            .First(tile => tile.character == START)
            .position;

        foreach (var (position, character) in map.allPositionsAndCharactersWithin) {
            if (character == SPLITTER) {
                splitters[position] = new Splitter(position);
            }
        }

        foreach (var (position, splitter) in splitters) {
            for (int y = position.y - 1; y >= start.y; y--) {
                if (map.Is(position.x, y, START)) {
                    splitter.isSplitting = true;
                    break;
                }

                if (map[position.x, y] == SPLITTER) {
                    break;
                }

                if (splitters.TryGetValue(new(position.x - 1, y), out var left)) {
                    splitter.ancestors.Add(left);
                }

                if (splitters.TryGetValue(new(position.x + 1, y), out var right)) {
                    splitter.ancestors.Add(right);
                }
            }
        }
    }

    readonly Dictionary<Vector2Int, Splitter> splitters = [];

    internal int splitCount => splitters.Values.Count(s => s.isSplitting);

    internal long timelineCount => 0;
}