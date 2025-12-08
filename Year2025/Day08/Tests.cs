using NUnit.Framework;

namespace Day08;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 10, 11)]
    public void Test_circuitCount(string file, int connectionCount, int expected) {
        Assert.That(new Runtime(file, connectionCount).circuitCount, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 10, 40)]
    public void Test_circuitAggregate(string file, int connectionCount, int expected) {
        Assert.That(new Runtime(file, connectionCount).circuitAggregate, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 25272)]
    public void Test_lastPairXProduct(string file, long expected) {
        Assert.That(new Runtime(file).lastPairXProduct, Is.EqualTo(expected));
    }
}