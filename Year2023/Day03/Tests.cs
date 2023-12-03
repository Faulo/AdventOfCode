using NUnit.Framework;

namespace Day03;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 4361)]
    public void GivenSchematic_WhenCalculateSumOfParts_ThenReturn(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfAdjacentParts, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 0, '4')]
    [TestCase("example-1.txt", 1, 0, '6')]
    [TestCase("example-1.txt", 0, 1, '.')]
    public void GivenSchematic_WhenTryGet_ThenReturnSymbol(string file, int x, int y, char expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGet(x, y, out char actual), Is.True);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", -1, 0)]
    [TestCase("example-1.txt", 10, 0)]
    [TestCase("example-1.txt", 0, -1)]
    [TestCase("example-1.txt", 0, 10)]
    public void GivenSchematic_WhenTryGetInvalidCoord_ThenReturnFalse(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGet(x, y, out _), Is.False);
    }
}