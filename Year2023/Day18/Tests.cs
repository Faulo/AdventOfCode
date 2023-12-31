using NUnit.Framework;
using Utilities;

namespace Day18;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 62)]
    public void Test_Runtime_TotalDigArea(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.totalDigArea, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 38)]
    public void Test_Runtime_PathLength(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.pathLength, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 24)]
    public void Test_Runtime_InsideArea(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.insideArea, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", "#######\r\n#ooooo#\r\n###ooo#\r\n..#ooo#\r\n..#ooo#\r\n###o###\r\n#ooo#..\r\n##oo###\r\n.#oooo#\r\n.######")]
    public void Test_Runtime_ToString(string file, string expected) {
        var sut = new Runtime(file);

        Assert.That(sut.ToString(), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 0, true)]
    [TestCase("example-1.txt", 1, 1, false)]
    [TestCase("example-1.txt", 0, 3, false)]
    [TestCase("example-1.txt", 3, 0, true)]
    public void Test_Runtime_IsOnPath(string file, int x, int y, bool expected) {
        var sut = new Runtime(file);

        Assert.That(sut.IsOnPath(new(x, y)), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1, 1)]
    [TestCase("input.txt", 64, 1)]
    public void Test_Runtime_FirstPositionInside(string file, int x, int y) {
        var sut = new Runtime(file);

        Assert.That(sut.firstPositionInside, Is.EqualTo(new Vector2Int(x, y)));
    }

    [TestCase("R 6 (#70c710)", 6, 0)]
    [TestCase("D 5 (#0dc571)", 0, 5)]
    [TestCase("L 2 (#5713f0)", -2, 0)]
    [TestCase("U 3 (#59c680)", 0, -3)]
    public void Test_Runtime_ParseLine(string line, int x, int y) {
        Assert.That(Runtime.ParseLine(line), Is.EqualTo(new Vector2Int(x, y)));
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