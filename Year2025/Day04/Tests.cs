using NUnit.Framework;

namespace Day04;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 13)]
    public void Test_accessibleRollCount(string file, int expected) {
        Assert.That(new Runtime(file).accessibleRollCount, Is.EqualTo(expected));
    }
    [TestCase("example.txt", 43)]
    public void Test_accessibleRollCountWithRemoving(string file, int expected) {
        Assert.That(new Runtime(file).accessibleRollCountWithRemoving, Is.EqualTo(expected));
    }
}