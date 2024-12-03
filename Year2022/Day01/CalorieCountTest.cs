using NUnit.Framework;
using NUnit.Framework.Legacy;

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
    [TestCase("input.txt", 66306)]
    public void TestFindMostCalories(string file, int expected) {
        Assert.That(CalorieCount.FindMostCalories(file), Is.EqualTo(expected));
    }

    [TestCase("example.txt", 45000)]
    [TestCase("input.txt", 195292)]
    public void TestFindSumOfTopThreeCalories(string file, int expected) {
        Assert.That(CalorieCount.FindSumOfTopThreeCalories(file), Is.EqualTo(expected));
    }
}