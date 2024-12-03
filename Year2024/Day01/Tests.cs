using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 11)]
    public void TestExample(string file, int expected) {
        Assert.That(new Runtime(file).totalDistance, Is.EqualTo(expected));
    }

    [TestCase(3, 4, 1)]
    [TestCase(7, 2, 5)]
    public void TestDelta(int left, int right, int expected) {
        Assert.That(Runtime.Delta(left, right), Is.EqualTo(expected));
    }
}