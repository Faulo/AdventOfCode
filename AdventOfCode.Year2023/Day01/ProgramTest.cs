using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(Program))]
public class ProgramTest {
    [TestCase("example.txt")]
    public void TestReadFileToArray(string file) {
        string[] expected = new[] {
            "1000",
            "2000",
            "3000", // 6000
            "",
            "4000", // 4000
            "",
            "5000",
            "6000", // 11000
            "",
            "7000",
            "8000",
            "9000", // 24000
            "",
            "10000" // 10000
        };

        CollectionAssert.AreEqual(expected, Program.ReadFileToArray(file));
    }

    [TestCase("example.txt")]
    public void ReadFileToElf(string file) {
        int[] expected = new[] {
            6000,
            4000,
            11000,
            24000,
            10000
        };

        CollectionAssert.AreEqual(expected, Program.ReadFileToElf(file));
    }

    [TestCase("example.txt", 24000)]
    public void CountCalories(string file, int expected) {
        Assert.AreEqual(expected, Program.FindMostCalories(file));
    }

    [Test]
    public void FindMostCalories() {
        Assert.Pass($"Calories: {Program.FindMostCalories("input.txt")}");
    }

    [Test]
    public void FindSumOfTopThreeCalories() {
        Assert.Pass($"Calories: {Program.FindSumOfTopThreeCalories("input.txt")}");
    }
}