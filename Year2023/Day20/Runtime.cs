using NUnit.Framework;
using Utilities;
using Pulse = (string source, string target, bool isHigh);

namespace Day20;

sealed class Runtime {
    internal long productOfPulses {
        get {
            var (lowSum, highSum) = pulseCounts;

            return lowSum * highSum;
        }
    }

    internal (long low, long high) pulseCounts {
        get {
            long lowSum = 0;
            long highSum = 0;

            var queue = new Queue<Pulse>();

            for (int i = 0; i < 1000; i++) {
                queue.Enqueue(("button", "broadcaster", false));

                do {
                    var pulse = queue.Dequeue();

                    if (pulse.isHigh) {
                        highSum++;
                    } else {
                        lowSum++;
                    }

                    if (modules.TryGetValue(pulse.target, out var module)) {
                        foreach (var output in module.SendPulse(pulse)) {
                            queue.Enqueue(output);
                        }
                    }
                } while (queue.Count > 0);
            }

            return (lowSum, highSum);
        }
    }

    internal long lowestButtonCount {
        get {
            long mp = GetLowestButtonCountFor(true.CreatePulse("mp", "dr"));
            long qt = GetLowestButtonCountFor(true.CreatePulse("qt", "dr"));
            long qb = GetLowestButtonCountFor(true.CreatePulse("qb", "dr"));
            long ng = GetLowestButtonCountFor(true.CreatePulse("ng", "dr"));

            return mp * qt * qb * ng;
        }
    }

    internal int GetLowestButtonCountFor(Pulse target) {
        int count = 0;
        var queue = new Queue<Pulse>();
        foreach (var module in modules.Values) {
            module.Reset();
        }

        while (count < short.MaxValue) {
            count++;
            queue.Enqueue(("button", "broadcaster", false));

            do {
                var pulse = queue.Dequeue();

                if (modules.TryGetValue(pulse.target, out var module)) {
                    foreach (var output in module.SendPulse(pulse)) {
                        if (output == target) {
                            return count;
                        }

                        queue.Enqueue(output);
                    }
                }
            } while (queue.Count > 0);
        }

        throw new Exception();
    }

    internal readonly Dictionary<string, Module> modules = [];

    internal Runtime(string file) {
        var button = new Module("button", "broadcaster");
        modules[button.name] = button;

        foreach (string line in new FileInput(file).ReadLines()) {
            var module = Module.Parse(line);
            modules[module.name] = module;
        }

        foreach (var module in modules.Values) {
            module.LinkModules(modules);
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

    internal virtual IEnumerable<Pulse> SendPulse(Pulse pulse) {
        return outputs.Select(m => (name, m, pulse.isHigh));
    }

    internal virtual void LinkModules(IDictionary<string, Module> modules) {
    }

    internal static Module Parse(string line) {
        string[] values = line.Split("->");
        string name = values[0].Trim();
        string[] outputs = values[1]
            .Split(',')
            .Select(o => o.Trim())
            .ToArray();

        return name[0] switch {
            '%' => new FlipFlopModule(name[1..], outputs),
            '&' => new ConjunctionModule(name[1..], outputs),
            _ => new Module(name, outputs)
        };
    }

    internal virtual void Reset() {
    }
}

class FlipFlopModule : Module {
    internal FlipFlopModule(string name, params string[] outputs) : base(name, outputs) {
    }

    internal bool isOn;

    internal override void Reset() {
        isOn = false;
    }

    internal override IEnumerable<Pulse> SendPulse(Pulse pulse) {
        if (pulse.isHigh) {
            return Enumerable.Empty<Pulse>();
        }

        isOn = !isOn;

        pulse.isHigh = isOn;

        return base.SendPulse(pulse);
    }
}

class ConjunctionModule : Module {
    readonly Dictionary<string, bool> inputs = [];
    internal ConjunctionModule(string name, params string[] outputs) : base(name, outputs) {
    }

    internal override void LinkModules(IDictionary<string, Module> modules) {
        foreach (var (name, module) in modules) {
            if (module.outputs.Contains(this.name)) {
                inputs[name] = false;
            }
        }
    }

    internal override void Reset() {
        foreach (string? i in inputs.Keys.ToList()) {
            inputs[i] = false;
        }
    }

    internal override IEnumerable<Pulse> SendPulse(Pulse pulse) {
        Assert.That(inputs, Contains.Key(pulse.source));
        inputs[pulse.source] = pulse.isHigh;

        pulse.isHigh = inputs.Values.Any(i => !i);

        return base.SendPulse(pulse);
    }
}

static class Extensions {
    internal static Pulse CreatePulse(this bool isHigh, string source, string target) {
        return (source, target, isHigh);
    }
    internal static Pulse CreatePulse(this bool isHigh, Module source, Module target) {
        return isHigh.CreatePulse(source.name, target.name);
    }
}