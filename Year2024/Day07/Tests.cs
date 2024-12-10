using NUnit.Framework;

namespace Day07;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 3749)]
    [TestCase("input.txt", 66343330034722)]
    public void SumOfTrue(string file, long expected) {
        Assert.That(new Runtime(file).sumOfTrue, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 11387)]
    [TestCase("input.txt", 637696070419031)]
    public void SumOfThree(string file, long expected) {
        Assert.That(new Runtime(file).sumOfThree, Is.EqualTo(expected));
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

    [TestCase(true, 190, 10, 19)]
    [TestCase(true, 3267, 81, 40, 27)]
    [TestCase(false, 83, 17, 5)]
    [TestCase(true, 156, 15, 6)]
    [TestCase(true, 7290, 6, 8, 6, 15)]
    [TestCase(false, 161011, 16, 10, 13)]
    [TestCase(true, 192, 17, 8, 14)]
    [TestCase(false, 21037, 9, 7, 18, 13)]
    [TestCase(true, 292, 11, 6, 16, 20)]
    public void CanBeThree(bool expected, long result, params long[] operands) {
        Assert.That(Runtime.CanBeThree(result, operands), Is.EqualTo(expected));
    }

    [TestCase("input.txt", 243410928)]
    [TestCase("input.txt", 247203131)]
    [TestCase("input.txt", 64771506201955)]
    public void SumOfTrue_GreaterThan(string file, long expected) {
        Assert.That(new Runtime(file).sumOfTrue, Is.GreaterThan(expected));
    }

    [TestCase("input.txt", 66343330034722)]
    public void SumOfThree_GreaterThan(string file, long expected) {
        Assert.That(new Runtime(file).sumOfThree, Is.GreaterThan(expected));
    }
}