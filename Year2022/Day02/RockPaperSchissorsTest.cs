using NUnit.Framework;

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
        Assert.AreEqual(expected, RockPaperScissors.CalculateLineScore(line, format));
    }

    [TestCase("A Y", MatchFormat.PickAndPick, 2)]
    [TestCase("A Y", MatchFormat.PickAndResult, 1)]
    [TestCase("B X", MatchFormat.PickAndPick, 1)]
    [TestCase("B X", MatchFormat.PickAndResult, 1)]
    [TestCase("C Z", MatchFormat.PickAndPick, 3)]
    [TestCase("C Z", MatchFormat.PickAndResult, 1)]
    public void TestCalculateLinePickScore(string line, MatchFormat format, int expected) {
        Assert.AreEqual(expected, RockPaperScissors.CalculateLinePickScore(line, format));
    }

    [TestCase("A Y", MatchFormat.PickAndPick, 6)]
    [TestCase("A Y", MatchFormat.PickAndResult, 3)]
    [TestCase("B X", MatchFormat.PickAndPick, 0)]
    [TestCase("B X", MatchFormat.PickAndResult, 0)]
    [TestCase("C Z", MatchFormat.PickAndPick, 3)]
    [TestCase("C Z", MatchFormat.PickAndResult, 6)]
    public void TestCalculateLineWinScore(string line, MatchFormat format, int expected) {
        Assert.AreEqual(expected, RockPaperScissors.CalculateLineWinScore(line, format));
    }

    [TestCase("example.txt", MatchFormat.PickAndPick, 15)]
    public void TestCalculateTotalScore(string file, MatchFormat format, int expected) {
        Assert.AreEqual(expected, RockPaperScissors.CalculateTotalScore(file, format));
    }

    [TestCase(MatchFormat.PickAndPick)]
    [TestCase(MatchFormat.PickAndResult)]
    public void CalculateTotalScore(MatchFormat format) {
        Assert.Pass($"Total Score: {RockPaperScissors.CalculateTotalScore("input.txt", format)}");
    }
}
