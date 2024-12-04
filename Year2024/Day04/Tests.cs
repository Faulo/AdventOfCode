using NUnit.Framework;

namespace Day04;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 18)]
    [TestCase("input.txt", 2358)]
    public void Straight(string file, int expected) {
        Assert.That(new Runtime(file).straightOccurences, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 9)]
    [TestCase("input.txt", 1737)]
    public void Cross(string file, int expected) {
        Assert.That(new Runtime(file).crossOccurences, Is.EqualTo(expected));
    }
}