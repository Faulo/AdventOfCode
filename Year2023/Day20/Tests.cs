using NUnit.Framework;

namespace Day20;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 32000000)]
    [TestCase("example-2.txt", 11687500)]
    public void Test_Runtime(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.productOfPulses, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 8000, 4000)]
    [TestCase("example-2.txt", 4250, 2750)]
    public void Test_Runtime_PulseCounts(string file, long expectedLow, long expectedHigh) {
        var sut = new Runtime(file);

        var expected = (expectedLow, expectedHigh);

        Assert.That(sut.pulseCounts, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 63237724691)]
    public void Test_Runtime_LowestButtonCount(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.lowestButtonCount, Is.GreaterThan(expected));
    }

    [TestCase("example-1.txt", 6)]
    [TestCase("example-2.txt", 6)]
    public void Test_Runtime_Module_Count(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.modules.Count, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0)]
    [TestCase("example-1.txt", 1)]
    [TestCase("example-1.txt", 2)]
    [TestCase("example-1.txt", 3)]
    [TestCase("example-1.txt", 4)]
    public void Test_Runtime_FlipFlopsAreOff(string file, int buttonCount) {
        var sut = new Runtime(file);

        for (int i = 0; i < buttonCount; i++) {
            _ = sut.pulseCounts;
        }

        foreach (var (name, module) in sut.modules) {
            if (module is FlipFlopModule flipFlop) {
                Assert.That(flipFlop.isOn, Is.EqualTo(false), $"FlipFlop '{name}' was on?!");
            }
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Test_Module_SendPulse(bool isHigh) {
        var broadcaster = new Module("broadcaster", "test");
        var sut = new Module("test", "a", "b");

        var pulse = isHigh.CreatePulse(broadcaster, sut);

        var expected = new[] {
            isHigh.CreatePulse(sut.name, "a"),
            isHigh.CreatePulse(sut.name, "b"),
        };

        Assert.That(sut.SendPulse(pulse), Is.EqualTo(expected));
    }

    [TestCase(false, true, false, false)]
    [TestCase(true, true, false, false)]
    [TestCase(false, false, true, true)]
    [TestCase(true, false, true, false)]
    public void Test_FlipFlopModule_SendPulse(bool initialState, bool isHigh, bool expectOutput, bool expectedPulse) {
        var broadcaster = new Module("broadcaster", "test");
        var sut = new FlipFlopModule("test", "a", "b") {
            isOn = initialState
        };

        var expected = expectOutput
            ? new[] {
                expectedPulse.CreatePulse(sut.name, "a"),
                expectedPulse.CreatePulse(sut.name, "b"),
            }
            : Array.Empty<(string, string, bool)>();

        var pulse = isHigh.CreatePulse(broadcaster, sut);
        Assert.That(sut.SendPulse(pulse), Is.EqualTo(expected));
    }

    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(2, false)]
    [TestCase(3, true)]
    public void Test_FlipFlopModule_SendPulses(int count, bool expected) {
        var broadcaster = new Module("broadcaster", "test");
        var sut = new FlipFlopModule("test", "a", "b");

        var pulse = false.CreatePulse(broadcaster, sut);
        for (int i = 0; i < count; i++) {
            sut.SendPulse(pulse);
        }

        Assert.That(sut.isOn, Is.EqualTo(expected));
    }

    [TestCase(false, true)]
    [TestCase(true, false)]
    public void Test_Conjunction_SendPulse(bool isHigh, bool expectedPulse) {
        var broadcaster = new Module("broadcaster", "test");
        var sut = new ConjunctionModule("test", "a", "b");
        sut.LinkModules(new Dictionary<string, Module> { [broadcaster.name] = broadcaster });

        var expected = new[] {
            expectedPulse.CreatePulse(sut.name, "a"),
            expectedPulse.CreatePulse(sut.name, "b"),
        };

        var pulse = isHigh.CreatePulse(broadcaster, sut);
        Assert.That(sut.SendPulse(pulse), Is.EqualTo(expected));
    }

    [TestCase(false, false, true)]
    [TestCase(false, true, true)]
    [TestCase(true, false, true)]
    [TestCase(true, true, false)]
    public void Test_Conjunction_SendPulses(bool aHigh, bool bHigh, bool expectedPulse) {
        var a = new Module("a", "test");
        var b = new Module("b", "test");
        var sut = new ConjunctionModule("test", "a", "b");
        sut.LinkModules(new Dictionary<string, Module> { [a.name] = a, [b.name] = b });

        var expectedA = new[] {
            true.CreatePulse(sut.name, "a"),
            true.CreatePulse(sut.name, "b"),
        };
        var expectedB = new[] {
            expectedPulse.CreatePulse(sut.name, "a"),
            expectedPulse.CreatePulse(sut.name, "b"),
        };

        var pulseA = aHigh.CreatePulse(a, sut);
        var pulseB = bHigh.CreatePulse(b, sut);
        Assert.That(sut.SendPulse(pulseA), Is.EqualTo(expectedA));
        Assert.That(sut.SendPulse(pulseB), Is.EqualTo(expectedB));
    }
}