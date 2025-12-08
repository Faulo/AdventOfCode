using Utilities;

namespace Day07;

sealed partial class Runtime {
    sealed class Splitter {
        readonly HashSet<Splitter> ancestors = [];

        readonly HashSet<Splitter> children = [];

        internal bool isSplitting {
            get => _isSplitting ??= ancestors.Any(s => s.isSplitting);
            set => _isSplitting = value;
        }

        bool? _isSplitting = null;

        internal void AddAncestor(Splitter ancestor) {
            ancestors.Add(ancestor);
            ancestor.children.Add(this);
        }

        internal long timelineCount {
            get => _timelineCount ??= children.Sum(s => s.timelineCount);
            set => _timelineCount = value;
        }

        long? _timelineCount = null;

        internal bool isStart => isSplitting && ancestors.Count == 0;
        internal bool isEnd => isSplitting && children.Count == 0;
        internal bool isVirtual = false;
    }

    const char START = 'S';
    const char SPLITTER = '^';

    readonly Dictionary<Vector2Int, Splitter> splitters = [];

    internal Runtime(string file) {
        var map = new FileInput(file).ReadAllAsCharacterMap();
        var start = map
            .allPositionsAndCharactersWithin
            .First(tile => tile.character == START)
            .position;

        foreach (var (position, character) in map.allPositionsAndCharactersWithin) {
            if (character == SPLITTER) {
                splitters[position] = new Splitter();
            }
        }

        for (int x = 0; x < map.width; x++) {
            splitters[new Vector2Int(x, map.height)] = new Splitter() {
                isVirtual = true,
                timelineCount = 1,
            };
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
                    splitter.AddAncestor(left);
                }

                if (splitters.TryGetValue(new(position.x + 1, y), out var right)) {
                    splitter.AddAncestor(right);
                }
            }
        }
    }

    internal int splitCount => splitters
        .Values
        .Count(s => !s.isVirtual && s.isSplitting);

    internal long timelineCount => splitters
        .Values
        .Where(s => s.isStart)
        .Sum(s => s.timelineCount);
}