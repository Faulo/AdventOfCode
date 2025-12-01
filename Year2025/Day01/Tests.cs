using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 3)]
    public void DialZeroCount(string file, int expected) {
        Assert.That(new Runtime(file).dialZeroCount, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 6)]
    public void PassZeroCount(string file, int expected) {
        Assert.That(new Runtime(file).passZeroCount, Is.EqualTo(expected));
    }
}