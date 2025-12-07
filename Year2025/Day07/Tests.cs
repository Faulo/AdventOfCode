using NUnit.Framework;

namespace Day07;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 21)]
    public void Test_splitCount(string file, int expected) {
        Assert.That(new Runtime(file).splitCount, Is.EqualTo(expected));
    }
}