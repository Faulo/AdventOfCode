using NUnit.Framework;

namespace Day14;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 136)]
    [TestCase("example-2.txt", 136)]
    public void Test_Runtime_NorthLoad(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.TiltNorth().northLoad, Is.EqualTo(expected));
    }
}