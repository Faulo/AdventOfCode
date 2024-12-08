using NUnit.Framework;

namespace Day08;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 14)]
    [TestCase("input.txt", 299)]
    public void SimpleAntinodeCount(string file, long expected) {
        Assert.That(new Runtime(file).simpleAntinodeCount, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 34)]
    public void ComplexAntinodeCount(string file, long expected) {
        Assert.That(new Runtime(file).complexAntinodeCount, Is.EqualTo(expected));
    }
}