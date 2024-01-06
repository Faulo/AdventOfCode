using NUnit.Framework;
using Utilities;

namespace Day24;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    const float ERROR_MARGIN = 1023f / 1024;

    [TestCase("example-1.txt", 7, 27, 2)]
    public void Test_Runtime_GetNumberOfCollisions(string file, long min, long max, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfCollisions(min, max), Is.EqualTo(expected));
    }
    [TestCase("input.txt", 200000000000000, 400000000000000, 13072)]
    public void Test_Runtime_GetNumberOfCollisions_GreaterThan(string file, long min, long max, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetNumberOfCollisions(min, max), Is.GreaterThan(expected));
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

    [TestCase("example-1.txt", 0, 1, 14.333, 15.333)]
    [TestCase("example-1.txt", 0, 2, 11.667, 16.667)]
    [TestCase("example-1.txt", 0, 3, 6.2, 19.4)]
    [TestCase("example-1.txt", 1, 3, -6, -5)]
    [TestCase("example-1.txt", 2, 3, -2, 3)]
    public void Test_Runtime_TryCalculateIntersection_Success(string file, int a, int b, double x, double y) {
        var sut = new Runtime(file);

        Assert.That(Runtime.TryCalculateIntersection(sut.hailstones[a], sut.hailstones[b], out var actual), Is.True);
        Assert.That(actual.x, Is.EqualTo(x).Within(ERROR_MARGIN));
        Assert.That(actual.y, Is.EqualTo(y).Within(ERROR_MARGIN));
    }

    [TestCase("example-1.txt", 0, 4)] // Hailstones' paths crossed in the past for hailstone A.
    [TestCase("example-1.txt", 1, 2)] // Hailstones' paths are parallel; they never intersect.
    [TestCase("example-1.txt", 1, 4)] // Hailstones' paths crossed in the past for both hailstones.
    [TestCase("example-1.txt", 2, 4)] // Hailstones' paths crossed in the past for hailstone B.
    [TestCase("example-1.txt", 3, 4)] // Hailstones' paths crossed in the past for both hailstones.
    public void Test_Runtime_TryCalculateIntersection_Failure(string file, int a, int b) {
        var sut = new Runtime(file);

        Assert.That(Runtime.TryCalculateIntersection(sut.hailstones[a], sut.hailstones[b], out _), Is.False);
    }
}