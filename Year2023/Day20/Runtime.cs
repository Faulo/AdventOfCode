using Utilities;

namespace Day20;

sealed class Runtime {
    internal long productOfPulses {
        get {
            long sum = 0;

            return sum;
        }
    }

    internal readonly Dictionary<string, Module> modules = [];

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            var module = Module.Parse(line);
            modules[module.name] = module;
        }
    }
}

class Module {

    internal readonly string name;

    internal readonly string[] outputs;

    internal Module(string name, params string[] outputs) {
        this.name = name;
        this.outputs = outputs;
    }

    internal static Module Parse(string line) {
        string[] values = line.Split("->");
        string name = values[0].Trim();
        string[] outputs = values[1]
            .Split(',')
            .Select(o => o.Trim())
            .ToArray();

        return name[0] switch {
            '%' => new FlipFlopModule(name, outputs),
            '&' => new ConjunctionModule(name, outputs),
            _ => new Module(name, outputs)
        };
    }
}

class FlipFlopModule : Module {
    internal FlipFlopModule(string name, params string[] outputs) : base(name, outputs) {
    }
}

class ConjunctionModule : Module {
    internal ConjunctionModule(string name, params string[] outputs) : base(name, outputs) {
    }
}

static class Extensions {
}