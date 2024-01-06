using NUnit.Framework;
using Utilities;

namespace Day24;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 7, 27, 2)]
    public void Test_Runtime_MaximumNumberOfSteps(string file, long min, long max, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfCollisions(min, max), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 5)]
    public void Test_Runtime_Hailstone_Count(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.hailstones.Count, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 19, 13, 30)]
    [TestCase("example-1.txt", 1, 18, 19, 22)]
    public void Test_Runtime_Hailstone_Position(string file, int index, long x, long y, long z) {
        var sut = new Runtime(file);

        Assert.That(sut.hailstones[index].position, Is.EqualTo(new Vector3Int(x, y, z)));
    }

    [TestCase("example-1.txt", 0, -2, 1, -2)]
    [TestCase("example-1.txt", 1, -1, -1, -2)]
    public void Test_Runtime_Hailstone_Velocity(string file, int index, long x, long y, long z) {
        var sut = new Runtime(file);

        Assert.That(sut.hailstones[index].velocity, Is.EqualTo(new Vector3Int(x, y, z)));
    }
}