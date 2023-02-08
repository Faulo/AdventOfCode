using NUnit.Framework;

namespace AdventOfCode.Year2023;

public class UnstableDiffusionTest {

    [TestCase("example.txt")]
    public void InputParsing(string file) {
        var input = new[] {
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
    }
}