using NUnit.Framework;

namespace Day06;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 41)]
    [TestCase("input.txt", 4656)]
    public void PathCount(string file, int expected) {
        Assert.That(new Runtime(file).pathCount, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 6)]
    [TestCase("input.txt", 1575)]
    public void ObstructionCount(string file, int expected) {
        Assert.That(new Runtime(file).obstructionCount, Is.EqualTo(expected));
    }
}