using NUnit.Framework;

namespace Day11;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 5)]
    [TestCase("input.txt", 758)]
    public void Test_youCount(string file, long expected) {
        Assert.That(new Runtime(file).youCount, Is.EqualTo(expected));
    }

    [TestCase("example-2.txt", 2)]
    public void Test_serverCount(string file, long expected) {
        Assert.That(new Runtime(file).serverCount, Is.EqualTo(expected));
    }
}