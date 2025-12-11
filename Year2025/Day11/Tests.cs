using NUnit.Framework;

namespace Day11;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 5)]
    public void Test_timelineCount(string file, long expected) {
        Assert.That(new Runtime(file).timelineCount, Is.EqualTo(expected));
    }
}