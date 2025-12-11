using Utilities;

namespace Day11;

sealed partial class Runtime {
    sealed class Splitter(string name) {
        internal readonly string name = name;

        public override string ToString() => name;

        readonly HashSet<Splitter> children = [];
        readonly HashSet<Splitter> ancestors = [];

        internal void AddAncestor(Splitter ancestor) {
            ancestors.Add(ancestor);
            ancestor.children.Add(this);
        }

        internal void AddChild(Splitter child) {
            children.Add(child);
            child.ancestors.Add(this);
        }

        internal long timelineCount {
            get => _timelineCount ??= children.Sum(s => s.timelineCount);
            set => _timelineCount = value;
        }

        long? _timelineCount = null;

        readonly long[] someTimelineCounts = [-1, -1, -1, -1];

        internal long GetSomeTimelineCount(int mask) {
            if (name == SERVER_DAC) {
                mask |= 1;
            }

            if (name == SERVER_FFT) {
                mask |= 2;
            }

            if (someTimelineCounts[mask] == -1) {
                someTimelineCounts[mask] = name == END
                    ? mask / 3
                    : children.Sum(s => s.GetSomeTimelineCount(mask));
            }

            return someTimelineCounts[mask];
        }
    }

    const string YOU = "you";
    const string SERVER = "svr";
    const string SERVER_DAC = "dac";
    const string SERVER_FFT = "fft";
    const string END = "out";

    readonly Dictionary<string, Splitter> splitters = [];

    Splitter Get(string name) {
        return splitters.TryGetValue(name, out var splitter)
            ? splitter
            : splitters[name] = new(name);
    }

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] args = line.Split(": ");
            string name = args[0];
            args = args[1].Split(' ');

            var splitter = Get(name);
            Array.ForEach(args, n => splitter.AddChild(Get(n)));
        }

        Get(END).timelineCount = 1;
    }

    internal long youCount => Get(YOU).timelineCount;

    internal long serverCount => Get(SERVER).GetSomeTimelineCount(0);
}