using NUnit.Framework;
using Utilities;

namespace Day21;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 0, 1)]
    [TestCase("example-1.txt", 1, 2)]
    [TestCase("example-1.txt", 2, 4)]
    [TestCase("example-1.txt", 3, 6)]
    [TestCase("example-1.txt", 6, 16)]
    public void Test_Runtime(string file, int steps, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfDestinations(steps), Is.EqualTo(expected));
    }

    [TestCase("input.txt", 26501365, true, 616951807954615)]
    public void Test_Runtime_LessThan(string file, int steps, bool wrap, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfDestinations(steps, wrap), Is.LessThan(expected));
    }

    [TestCase("input.txt", 26501365, true, 616951786718660)]
    public void Test_Runtime_GreaterThan(string file, int steps, bool wrap, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfDestinations(steps, wrap), Is.GreaterThan(expected));
    }

    [TestCase("example-1.txt", 5, 5)]
    public void Test_Runtime_Start(string file, int x, int y) {
        var sut = new Runtime(file);

        Assert.That(sut.start, Is.EqualTo(new Vector2Int(x, y)));
    }
    [TestCase("example-1.txt", 81)]
    public void Test_Runtime_FreeSpots(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.freeSpots, Is.EqualTo(expected));
    }
}