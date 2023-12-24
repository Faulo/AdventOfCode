using NUnit.Framework;

namespace Day11;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 374)]
    public void Test_Runtime_SumOfShortestPaths(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfShortestPaths, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 12)]
    public void Test_Runtime_Width(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.width, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 15)]
    public void Test_Runtime_Height(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.height, Is.EqualTo(expected));
    }
}