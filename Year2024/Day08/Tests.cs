using NUnit.Framework;

namespace Day08;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 14)]
    public void SumOfTrue(string file, long expected) {
        Assert.That(new Runtime(file).antinodeCount, Is.EqualTo(expected));
    }
}