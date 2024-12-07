using NUnit.Framework;

namespace Day07;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 3749)]
    public void SumOfTrue(string file, int expected) {
        Assert.That(new Runtime(file).sumOfTrue, Is.EqualTo(expected));
    }

    [TestCase(true, 190, 10, 19)]
    [TestCase(true, 3267, 81, 40, 27)]
    [TestCase(false, 83, 17, 5)]
    [TestCase(false, 156, 15, 6)]
    [TestCase(false, 7290, 6, 8, 6, 15)]
    [TestCase(false, 161011, 16, 10, 13)]
    [TestCase(false, 192, 17, 8, 14)]
    [TestCase(false, 21037, 9, 7, 18, 13)]
    [TestCase(true, 292, 11, 6, 16, 20)]
    public void CanBeTrue(bool expected, long result, params long[] operands) {
        Assert.That(Runtime.CanBeTrue(result, operands), Is.EqualTo(expected));
    }

    [TestCase("input.txt", 243410928)]
    [TestCase("input.txt", 247203131)]
    [TestCase("input.txt", 64771506201955)]
    public void SumOfTrue_GreaterThan(string file, long expected) {
        Assert.That(new Runtime(file).sumOfTrue, Is.GreaterThan(expected));
    }
}