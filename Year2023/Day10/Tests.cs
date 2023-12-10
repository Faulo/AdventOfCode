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
}