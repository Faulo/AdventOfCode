using NUnit.Framework;

namespace Day04;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 13)]
    public void TestCalculateSumOfWins(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfWins, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 30)]
    public void TestCalculateSumOfCards(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfCards, Is.EqualTo(expected));
    }

    [TestCase("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 1, 4)]
    [TestCase("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2, 2)]
    [TestCase("Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 3, 2)]
    [TestCase("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 4, 1)]
    [TestCase("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 5, 0)]
    [TestCase("Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 6, 0)]
    public void TestCalculateWins(string line, int expectedId, int expectedWins) {
        Assert.That(Runtime.CalculateWins(line), Is.EqualTo((expectedId, expectedWins)));
    }

    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(3, 4)]
    public void TestCalculatePowerOfWins(int wins, int expected) {
        Assert.That(Runtime.CalculatePowerOfWins(wins), Is.EqualTo(expected));
    }
}