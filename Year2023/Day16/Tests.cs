using Day16;
using NUnit.Framework;

namespace Day15;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 46)]
    public void Test_Runtime_SumOfHashes(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.numberOfEnergizedTiles, Is.EqualTo(expected));
    }
}