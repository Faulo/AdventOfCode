using NUnit.Framework;

namespace Day03;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 2, 357)]
    [TestCase("example.txt", 12, 3121910778619)]
    [TestCase("input.txt", 12, 172162399742349)]
    public void Test_totalJoltage(string file, int count, long expected) {
        Assert.That(new Runtime(file, count).totalJoltage, Is.EqualTo(expected));
    }

    [TestCase("987654321111111", 98)]
    [TestCase("818181911112111", 92)]
    public void Test_sumOfAllInvalidIds(string bank, int expected) {
        Assert.That(Runtime.FindHighestJoltage(bank), Is.EqualTo(expected));
    }
}