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
    [TestCase("example-1.txt", 10, 50)]
    [TestCase("example-1.txt", 50, 1594)]
    [TestCase("example-1.txt", 100, 6536)]
    [TestCase("example-1.txt", 500, 167004)]
    [TestCase("example-1.txt", 1000, 668697)]
    [TestCase("example-1.txt", 5000, 16733044)]
    public void Test_Runtime(string file, int steps, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfDestinations(steps), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 5, 5)]
    public void Test_Runtime_Start(string file, int x, int y) {
        var sut = new Runtime(file);

        Assert.That(sut.start, Is.EqualTo(new Vector2Int(x, y)));
    }
}