using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(Program))]
public class ProgramTest {
    [TestCase("example.txt")]
    public void InputParsing(string file) {
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

    [Test]
    public void ProcessInput() {
        Assert.Pass($"Calories: {Program.CountCalories("input.txt")}");
    }
}