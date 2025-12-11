using Utilities;

namespace Day11;

sealed partial class Runtime {
    sealed class Splitter {
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
            get => _timelineCount ??= isEnd ? 1 : children.Sum(s => s.timelineCount);
            private set => _timelineCount = value;
        }

        long? _timelineCount = null;

        internal bool isStart;
        internal bool isEnd;
    }

    const string START = "you";
    const string END = "out";

    readonly Dictionary<string, Splitter> splitters = [];

    Splitter Get(string name) {
        return splitters.TryGetValue(name, out var splitter)
            ? splitter
            : splitters[name] = new();
    }

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] args = line.Split(": ");
            string name = args[0];
            args = args[1].Split(' ');

            var splitter = Get(name);
            Array.ForEach(args, n => splitter.AddChild(Get(n)));
        }

        Get(START).isStart = true;
        Get(END).isEnd = true;
    }

    internal long timelineCount => splitters
        .Values
        .Where(s => s.isStart)
        .Sum(s => s.timelineCount);
}