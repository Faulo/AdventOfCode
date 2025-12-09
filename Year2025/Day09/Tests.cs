using NUnit.Framework;

namespace Day09;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 50)]
    public void Test_largestRectangleArea(string file, long expected) {
        Assert.That(new Runtime(file).largestRectangleArea, Is.EqualTo(expected));
    }
}