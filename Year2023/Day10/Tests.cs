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

    [TestCase("example-3.txt", 4)]
    [TestCase("example-4.txt", 8)]
    [TestCase("example-5.txt", 10)]
    public void Test_Runtime_EnclosedArea(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.enclosedArea, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1, 1)]
    [TestCase("example-2.txt", 0, 2)]
    public void Test_Runtime_Start(string file, int expectedX, int expectedY) {
        var runtime = new Runtime(file);

        Assert.That(runtime.start, Is.EqualTo(new Vector2(expectedX, expectedY)));
    }

    [TestCase("example-1.txt", 'F')]
    [TestCase("example-2.txt", 'F')]
    [TestCase("example-3.txt", 'F')]
    [TestCase("example-4.txt", 'F')]
    [TestCase("example-5.txt", '7')]
    public void Test_Runtime_StartCharacter(string file, char expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime[runtime.start], Is.EqualTo(expected));
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

    [TestCase("example-3.txt", 1, 1)]
    [TestCase("example-3.txt", 1, 2)]
    [TestCase("example-3.txt", 1, 3)]
    [TestCase("example-3.txt", 1, 4)]
    [TestCase("example-3.txt", 1, 5)]
    [TestCase("example-3.txt", 1, 6)]
    [TestCase("example-3.txt", 1, 7)]
    public void Test_Runtime_IsOnpath(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.IsOnPath(new Vector2(x, y)), Is.True);
    }

    [TestCase("example-3.txt", 1, 8)]
    [TestCase("example-3.txt", 9, 8)]
    [TestCase("example-3.txt", 4, 8)]
    [TestCase("example-3.txt", 6, 8)]
    public void Test_Runtime_IsNotOnPath(string file, int x, int y) {
        var runtime = new Runtime(file);

        Assert.That(runtime.IsOnPath(new Vector2(x, y)), Is.False);
    }
}