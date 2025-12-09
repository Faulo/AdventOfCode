using NUnit.Framework;

namespace Day09;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 50)]
    public void Test_largestRectangleArea(string file, long expected) {
        Assert.That(new Runtime(file).largestRectangleArea, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 7, 0, false)]
    [TestCase("example.txt", 6, 1, false)]
    [TestCase("example.txt", 7, 1, true)]
    [TestCase("example.txt", 8, 1, true)]
    [TestCase("example.txt", 9, 1, true)]
    [TestCase("example.txt", 10, 1, true)]
    [TestCase("example.txt", 11, 1, true)]
    [TestCase("example.txt", 12, 1, false)]
    [TestCase("example.txt", 7, 2, true)]
    [TestCase("example.txt", 8, 2, true)]
    [TestCase("example.txt", 9, 2, true)]
    [TestCase("example.txt", 10, 2, true)]
    [TestCase("example.txt", 11, 2, true)]
    public void Test_IsGreenOrRed(string file, int x, int y, bool expected) {
        Assert.That(new Runtime(file).IsGreenOrRed(new(x, y)), Is.EqualTo(expected));
    }

    [TestCase("example.txt", 24)]
    public void Test_largestRectangleAreaOnlyGreen(string file, long expected) {
        Assert.That(new Runtime(file).largestRectangleAreaOnlyGreen, Is.EqualTo(expected));
    }
}