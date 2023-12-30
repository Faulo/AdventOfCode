using NUnit.Framework;

namespace Day17;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 102)]
    public void Test_Runtime_MininumHeatLoss(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.mininumHeatLoss, Is.EqualTo(expected));
    }
}