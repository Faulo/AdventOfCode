using NUnit.Framework;

namespace Day24;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 7, 27, 2)]
    public void Test_Runtime_MaximumNumberOfSteps(string file, long min, long max, long expected) {
        var sut = new Runtime(file, min, max);

        Assert.That(sut.numberOfCollisions, Is.EqualTo(expected));
    }
}