using NUnit.Framework;

namespace Day07;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 21)]
    [TestCase("input.txt", 1587)]
    public void Test_splitCount(string file, int expected) {
        Assert.That(new Runtime(file).splitCount, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 40)]
    [TestCase("input.txt", 5748679033029)]
    public void Test_timelineCount(string file, long expected) {
        Assert.That(new Runtime(file).timelineCount, Is.EqualTo(expected));
    }
}