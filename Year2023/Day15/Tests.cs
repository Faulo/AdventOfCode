using NUnit.Framework;

namespace Day15;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 1320)]
    public void Test_Runtime_SumOfHashes(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfHashes, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 506805)]
    public void Test_Runtime_SumOfHashes_GreaterThan(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfHashes, Is.GreaterThan(expected));
    }

    [TestCase("rn=1", 30)]
    [TestCase("cm-", 253)]
    [TestCase("qp=3", 97)]
    [TestCase("cm=2", 47)]
    [TestCase("qp-", 14)]
    [TestCase("pc=4", 180)]
    [TestCase("ot=9", 9)]
    [TestCase("ab=5", 197)]
    [TestCase("pc-", 48)]
    [TestCase("pc=6", 214)]
    [TestCase("ot=7", 231)]
    public void Test_Runtime_Hash(string value, int expected) {
        var sut = new Hash(value);

        Assert.That(sut.GetHashCode(), Is.EqualTo(expected));
    }
}