using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(CalorieCount))]
public class CalorieCountTest {
    [TestCase(
        "example.txt",
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
    )]
    public void TestReadFileToArray(string file, params string[] expected) {
        CollectionAssert.AreEqual(expected, CalorieCount.ReadFileToArray(file));
    }

    [TestCase("example.txt", 6000, 4000, 11000, 24000, 10000)]
    public void TestReadFileToElf(string file, params int[] expected) {
        CollectionAssert.AreEqual(expected, CalorieCount.ReadFileToElf(file));
    }

    [TestCase("example.txt", 24000)]
    public void TestFindMostCalories(string file, int expected) {
        Assert.AreEqual(expected, CalorieCount.FindMostCalories(file));
    }

    [Test]
    public void FindMostCalories() {
        Assert.Pass($"Calories: {CalorieCount.FindMostCalories("input.txt")}");
    }

    [Test]
    public void FindSumOfTopThreeCalories() {
        Assert.Pass($"Calories: {CalorieCount.FindSumOfTopThreeCalories("input.txt")}");
    }
}