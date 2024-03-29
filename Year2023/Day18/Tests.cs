using NUnit.Framework;
using Utilities;

namespace Day18;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", false, 62)]
    [TestCase("example-1.txt", true, 952408144115)]
    public void Test_Runtime_TotalDigArea(string file, bool useColor, long expected) {
        var sut = new Runtime(file, useColor);

        Assert.That(sut.totalDigArea, Is.EqualTo(expected));
    }

    [TestCase("R 6 (#70c710)", 6, 0)]
    [TestCase("D 5 (#0dc571)", 0, 5)]
    [TestCase("L 2 (#5713f0)", -2, 0)]
    [TestCase("U 3 (#59c680)", 0, -3)]
    public void Test_Runtime_ParseLine(string line, int x, int y) {
        Assert.That(Runtime.ParseLine(line), Is.EqualTo(new Vector2Int(x, y)));
    }

    [TestCase("R 6 (#70c710)", 461937, 0)]
    [TestCase("D 5 (#0dc571)", 0, 56407)]
    [TestCase("L 2 (#8ceee2)", -577262, 0)]
    [TestCase("U 3 (#caa173)", 0, -829975)]
    public void Test_Runtime_ParseColor(string line, int x, int y) {
        Assert.That(Runtime.ParseColor(line), Is.EqualTo(new Vector2Int(x, y)));
    }

    [Test]
    public void Test_Runtime_TransposeToPositive() {
        List<Vector2Int> path = [
            new Vector2Int(0, 0),
            new Vector2Int(-1, 1),
            new Vector2Int(2, -2),
        ];

        List<Vector2Int> expected = [
            new Vector2Int(1, 2),
            new Vector2Int(0, 3),
            new Vector2Int(3, 0),
        ];

        Runtime.TransposeToPositive(path);

        Assert.That(path, Is.EqualTo(expected));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(50)]
    public void Test_Runtime_ScaleToSmallest(int expected) {
        List<Vector2Int> path = [
            new Vector2Int(0, 0),
            new Vector2Int(1, 7),
            new Vector2Int(3, 15),
        ];
        var scaledPath = path
            .Select(p => p * expected)
            .ToList();

        long scale = Runtime.ScaleToSmallest(scaledPath);

        Assert.That(scale, Is.EqualTo(expected));
        Assert.That(scaledPath, Is.EqualTo(path));
    }

    [Test]
    public void Test_Runtime_MoveBetween_X() {
        var start = new Vector2Int(0, 0);
        var goal = new Vector2Int(3, 0);

        List<Vector2Int> expected = [
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(2, 0),
        ];

        Assert.That(Runtime.MoveBetween(start, goal).ToList(), Is.EqualTo(expected));
    }

    [Test]
    public void Test_Runtime_MoveBetween_Y() {
        var start = new Vector2Int(0, 3);
        var goal = new Vector2Int(0, 0);

        List<Vector2Int> expected = [
            new Vector2Int(0, 3),
            new Vector2Int(0, 2),
            new Vector2Int(0, 1),
        ];

        Assert.That(Runtime.MoveBetween(start, goal).ToList(), Is.EqualTo(expected));
    }
}