using NUnit.Framework;

namespace Day10;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 7)]
    [TestCase("input.txt", 502)]
    public void Test_toggleCountSum(string file, long expected) {
        Assert.That(new Runtime(file).toggleCountSum, Is.EqualTo(expected));
    }

    [TestCase("0", 1)]
    [TestCase("1,2", 2 | 4)]
    public void Test_ParseButtons(string button, byte expected) {
        Assert.That(Runtime.ParseButtons([button]), Is.EqualTo(new ulong[] { expected }));
    }

    [TestCase(".##.", 2 | 4)]
    [TestCase("#..#", 1 | 8)]
    [TestCase(".", 0)]
    public void Test_ParseLight(string lights, byte expected) {
        Assert.That(Runtime.ParseLights(lights), Is.EqualTo(expected));
    }

    [TestCase("example.txt", 33)]
    public void Test_addCountSum(string file, long expected) {
        Assert.That(new Runtime(file).addCountSum, Is.EqualTo(expected));
    }
}