using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("1abc2", 12)]
    [TestCase("pqr3stu8vwx", 38)]
    [TestCase("a1b2c3d4e5f", 15)]
    [TestCase("treb7uchet", 77)]
    [TestCase("two1nine", 29)]
    [TestCase("eightwothree", 83)]
    [TestCase("abcone2threexyz", 13)]
    [TestCase("xtwone3four", 24)]
    [TestCase("4nineeightseven2", 42)]
    [TestCase("pqrstsixteen", 14)]
    [TestCase("7pqrstsixteen", 76)]
    public void GivenLine_ThenFindDigits(string input, int expected) {
        Assert.That(Runtime.FindCalibration(input), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 142)]
    [TestCase("example-2.txt", 281)]
    public void GivenFile_WhenGetCalibrationSum_ThenReturn(string file, int expected) {
        Assert.That(new Runtime(file).calibrationSum, Is.EqualTo(expected));
    }
}