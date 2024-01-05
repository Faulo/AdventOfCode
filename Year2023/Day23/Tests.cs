using NUnit.Framework;

namespace Day23;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 94)]
    public void Test_Runtime_MaximumNumberOfSteps(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.maximumNumberOfSteps, Is.EqualTo(expected));
    }
}