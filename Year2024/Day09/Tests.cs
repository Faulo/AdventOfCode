using NUnit.Framework;

namespace Day09;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 1928)]
    public void DefragChecksum(string file, long expected) {
        Assert.That(new Runtime(file).defragChecksum, Is.EqualTo(expected));
    }
}