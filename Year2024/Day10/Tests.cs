using NUnit.Framework;

namespace Day10;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 36)]
    public void TrailheadCount(string file, long expected) {
        Assert.That(new Runtime(file).trailheadCount, Is.EqualTo(expected));
    }
}