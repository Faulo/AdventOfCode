using NUnit.Framework;

namespace Day14;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 136)]
    [TestCase("example-2.txt", 136)]
    [TestCase("input.txt", 110090)]
    public void Test_Runtime_NorthLoad(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.TiltNorth().northLoad, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1000, 64)]
    [TestCase("example-1.txt", 1000000000, 64)]
    public void Test_Runtime_CycleLoad(string file, int cycles, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.CycleTilt(cycles).northLoad, Is.EqualTo(expected));
    }
}