using NUnit.Framework;

namespace Day08;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 14)]
    [TestCase("input.txt", 299)]
    public void AntinodeCount(string file, long expected) {
        Assert.That(new Runtime(file).antinodeCount, Is.EqualTo(expected));
    }
}