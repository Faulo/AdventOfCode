using NUnit.Framework;

namespace Day03;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 4361)]
    public void GivenSchematic_WhenCalculateSumOfParts_ThenReturn(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfAdjacentParts, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 467835)]
    public void GivenSchematic_WhenCalculateSumOfGears_ThenReturn(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfAdjacentGears, Is.EqualTo(expected));
    }
    [TestCase("input.txt", 538120)]
    public void GivenSchematic_WhenCalculateSumOfParts_ThenReturnLess(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfAdjacentParts, Is.LessThan(expected));
    }

    [TestCase("example-1.txt", 0, 0, '4')]
    [TestCase("example-1.txt", 1, 0, '6')]
    [TestCase("example-1.txt", 0, 1, '.')]
    public void GivenSchematic_WhenTryGet_ThenReturnSymbol(string file, int x, int y, char expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetSymbol(x, y, out char actual), Is.True);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", -1, 0)]
    [TestCase("example-1.txt", 10, 0)]
    [TestCase("example-1.txt", 0, -1)]
    [TestCase("example-1.txt", 0, 10)]
    public void GivenSchematic_WhenTryGetInvalidCoord_ThenReturnFalse(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetSymbol(x, y, out _), Is.False);
    }

    [TestCase("example-1.txt", 0, 0, 467)]
    [TestCase("example-1.txt", 2, 2, 35)]
    [TestCase("example-1.txt", 6, 2, 633)]
    public void GivenSchematic_WhenTryGetAdjacentPart_ThenReturnAdjacentPart(string file, int x, int y, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetAdjacentPart(x, y, out int actual), Is.True);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 5, 0)]
    [TestCase("example-1.txt", 1, 0)]
    [TestCase("example-1.txt", 2, 0)]
    [TestCase("example-1.txt", 3, 2)]
    public void GivenSchematic_WhenTryGetAdjacentPartInvalidCoord_ThenReturnFalse(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetAdjacentPart(x, y, out _), Is.False);
    }

    [TestCase("example-1.txt", 1, 0, 467)]
    [TestCase("example-1.txt", 2, 0, 467)]
    [TestCase("example-1.txt", 3, 2, 35)]
    public void GivenSchematic_WhenTryGetAdjacentPartInvalidCoordButWalkLeft_ThenReturnAdjacentPart(string file, int x, int y, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetAdjacentPart(x, y, out int actual, true), Is.True);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 10, 10)]
    [TestCase("input.txt", 140, 140)]
    public void GivenSchematic_WhenTryGetSize_ThenReturnWidthAndHeight(string file, int width, int height) {
        var runtime = new Runtime(file);

        Assert.That((runtime.width, runtime.height), Is.EqualTo((width, height)));
    }

    [TestCase("example-1.txt", 3, 1, 16345)]
    [TestCase("example-1.txt", 5, 8, 451490)]
    public void GivenSchematic_WhenTryGetAdjacentGear_ThenReturnAdjacentGear(string file, int x, int y, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetAdjacentGear(x, y, out int actual), Is.True);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 0)]
    [TestCase("example-1.txt", 3, 4)]
    public void GivenSchematic_WhenTryGetAdjacentGearInvalidCoords_ThenReturnFalse(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.TryGetAdjacentGear(x, y, out _), Is.False);
    }
}