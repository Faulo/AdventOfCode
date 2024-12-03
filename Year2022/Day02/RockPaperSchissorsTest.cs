using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Day02;

class RockPaperSchissorsTest {
    [TestCase(
        "example.txt",
        "A Y",
        "B X",
        "C Z"
    )]
    public void TestReadFileToArray(string file, params string[] expected) {
        CollectionAssert.AreEqual(expected, RockPaperScissors.ReadFileToArray(file));
    }

    [TestCase("A Y", MatchFormat.PickAndPick, 8)]
    [TestCase("A Y", MatchFormat.PickAndResult, 4)]
    [TestCase("B X", MatchFormat.PickAndPick, 1)]
    [TestCase("B X", MatchFormat.PickAndResult, 1)]
    [TestCase("C Z", MatchFormat.PickAndPick, 6)]
    [TestCase("C Z", MatchFormat.PickAndResult, 7)]
    public void TestCalculateLineScore(string line, MatchFormat format, int expected) {
        Assert.That(RockPaperScissors.CalculateLineScore(line, format), Is.EqualTo(expected));
    }

    [TestCase("A Y", MatchFormat.PickAndPick, 2)]
    [TestCase("A Y", MatchFormat.PickAndResult, 1)]
    [TestCase("B X", MatchFormat.PickAndPick, 1)]
    [TestCase("B X", MatchFormat.PickAndResult, 1)]
    [TestCase("C Z", MatchFormat.PickAndPick, 3)]
    [TestCase("C Z", MatchFormat.PickAndResult, 1)]
    public void TestCalculateLinePickScore(string line, MatchFormat format, int expected) {
        Assert.That(RockPaperScissors.CalculateLinePickScore(line, format), Is.EqualTo(expected));
    }

    [TestCase("A Y", MatchFormat.PickAndPick, 6)]
    [TestCase("A Y", MatchFormat.PickAndResult, 3)]
    [TestCase("B X", MatchFormat.PickAndPick, 0)]
    [TestCase("B X", MatchFormat.PickAndResult, 0)]
    [TestCase("C Z", MatchFormat.PickAndPick, 3)]
    [TestCase("C Z", MatchFormat.PickAndResult, 6)]
    public void TestCalculateLineWinScore(string line, MatchFormat format, int expected) {
        Assert.That(RockPaperScissors.CalculateLineWinScore(line, format), Is.EqualTo(expected));
    }

    [TestCase("example.txt", MatchFormat.PickAndPick, 15)]
    [TestCase("example.txt", MatchFormat.PickAndResult, 12)]
    [TestCase("input.txt", MatchFormat.PickAndPick, 17189)]
    [TestCase("input.txt", MatchFormat.PickAndResult, 13490)]
    public void TestCalculateTotalScore(string file, MatchFormat format, int expected) {
        Assert.That(RockPaperScissors.CalculateTotalScore(file, format), Is.EqualTo(expected));
    }
}
