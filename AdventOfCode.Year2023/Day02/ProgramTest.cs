using NUnit.Framework;

namespace Day02;

class ProgramTest {
    [TestCase(
        "example.txt",
        "A Y",
        "B X",
        "C Z"
    )]
    public void TestReadFileToArray(string file, params string[] expected) {
        CollectionAssert.AreEqual(expected, Program.ReadFileToArray(file));
    }

    [TestCase("A Y", 8)]
    [TestCase("B X", 1)]
    [TestCase("C Z", 6)]
    public void TestCalculateLineScore(string line, int expected) {
        Assert.AreEqual(expected, Program.CalculateLineScore(line));
    }

    [TestCase("A X", 1)]
    [TestCase("A Y", 2)]
    [TestCase("A Z", 3)]
    public void TestCalculateLinePickScore(string line, int expected) {
        Assert.AreEqual(expected, Program.CalculateLinePickScore(line));
    }

    [TestCase("A Y", 6)]
    [TestCase("B X", 0)]
    [TestCase("C Z", 3)]
    public void TestCalculateLineWinScore(string line, int expected) {
        Assert.AreEqual(expected, Program.CalculateLineWinScore(line));
    }

    [TestCase("example.txt", 15)]
    public void TestCalculateTotalScore(string file, int expected) {
        Assert.AreEqual(expected, Program.CalculateTotalScore(file));
    }

    [Test]
    public void CalculateTotalScore() {
        Assert.Pass($"Total Score: {Program.CalculateTotalScore("input.txt")}");
    }
}
