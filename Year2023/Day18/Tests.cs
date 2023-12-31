using NUnit.Framework;

namespace Day18;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 62)]
    public void Test_Runtime_TotalDigArea(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.totalDigArea, Is.EqualTo(expected));
    }
}