using NUnit.Framework;
using Utilities;

namespace Day11;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 374)]
    public void Test_Runtime_SumOfShortestPaths(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfShortestPaths, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 10, 1030)]
    [TestCase("example-1.txt", 100, 8410)]
    public void Test_Runtime_WithExpansion_SumOfShortestPaths(string file, int expansion, int expected) {
        var runtime = new Runtime(file, expansion);

        Assert.That(runtime.sumOfShortestPaths, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 2)]
    [TestCase("example-1.txt", 3, 0)]
    public void Test_Runtime_Galaxy(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.galaxies, Contains.Item(new Vector2Int(x, y)));
    }

    [TestCase("example-1.txt", 0, 2, 0, 2, 0)]
    [TestCase("example-1.txt", 0, 2, 3, 0, 6)]
    [TestCase("example-1.txt", 1, 5, 4, 9, 9)]
    public void Test_Runtime_CalculateDistance(string file, int x1, int y1, int x2, int y2, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.CalculateDistance(new(x1, y1), new(x2, y2)), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 12)]
    public void Test_Runtime_Width(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.expandedWidth, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 13)]
    public void Test_Runtime_Height(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.expandedHeight, Is.EqualTo(expected));
    }
}