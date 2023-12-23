using NUnit.Framework;

namespace Day10;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 4)]
    [TestCase("example-2.txt", 8)]
    public void Test_Runtime_MaximumDistance(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maximumDistance, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1, 1)]
    [TestCase("example-2.txt", 0, 2)]
    public void Test_Runtime_Start(string file, int expectedX, int expectedY) {
        var runtime = new Runtime(file);

        Assert.That(runtime.start, Is.EqualTo(new Vector2(expectedX, expectedY)));
    }

    [TestCase("example-1.txt", 1, 1, 2, 1)]
    [TestCase("example-1.txt", 1, 1, 1, 2)]
    [TestCase("example-2.txt", 0, 2, 1, 2)]
    [TestCase("example-2.txt", 0, 2, 0, 3)]
    public void Test_Runtime_GetNeighborsPointingTo(string file, int x, int y, int expectedX, int expectedY) {
        var runtime = new Runtime(file);

        Assert.That(runtime.GetNeighborsPointingTo(new Vector2(x, y)), Contains.Item(new Vector2(expectedX, expectedY)));
    }

    [TestCase("example-1.txt", 2, 1, 1, 1)]
    [TestCase("example-1.txt", 1, 2, 1, 1)]
    [TestCase("example-2.txt", 1, 2, 0, 2)]
    [TestCase("example-2.txt", 0, 3, 0, 2)]
    public void Test_Runtime_GetNeighborsPointingFrom(string file, int x, int y, int expectedX, int expectedY) {
        var runtime = new Runtime(file);

        Assert.That(runtime.GetNeighborsPointingFrom(new Vector2(x, y)), Contains.Item(new Vector2(expectedX, expectedY)));
    }
}