using NUnit.Framework;

namespace Day01;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("1abc2", 12)]
    [TestCase("pqr3stu8vwx", 38)]
    [TestCase("a1b2c3d4e5f", 15)]
    [TestCase("treb7uchet", 77)]
    public void GivenLine_ThenFindDigits(string input, int expected) {
        Assert.That(Runtime.FindCalibration(input), Is.EqualTo(expected));
    }

    [TestCase("example.txt", 142)]
    public void GivenFile_WhenGetCalibrationSum_ThenReturn(string file, int expected) {
        Assert.That(new Runtime(file).calibrationSum, Is.EqualTo(expected));
    }
}