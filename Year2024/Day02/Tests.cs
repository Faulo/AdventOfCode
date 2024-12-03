using NUnit.Framework;

namespace Day02;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 2)]
    [TestCase("input.txt", 524)]
    public void SafeReports(string file, int expected) {
        Assert.That(new Runtime(file).safeReports, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 4)]
    [TestCase("input.txt", 569)]
    public void SafeReportsWithDampener(string file, int expected) {
        Assert.That(new Runtime(file, true).safeReports, Is.EqualTo(expected));
    }

    [TestCase(10, 10, 0)]
    [TestCase(10, 11, 1)]
    [TestCase(10, 13, 1)]
    [TestCase(10, 14, 0)]
    [TestCase(14, 10, 0)]
    [TestCase(13, 10, -1)]
    [TestCase(11, 10, -1)]
    [TestCase(2, 1, -1)]
    [TestCase(0, 0, 0)]
    [TestCase(2, 7, 0)]
    [TestCase(6, 2, 0)]
    public void SafeScore(int left, int right, int expected) {
        Assert.That(Runtime.SafeScore(left, right), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 7, 6, 4, 2, 1)]
    [TestCase("example-1.txt", 1, 1, 2, 7, 8, 9)]
    public void MapRow(string file, int index, params int[] expected) {
        Assert.That(new Runtime(file).map[index], Is.EqualTo(expected));
    }
}