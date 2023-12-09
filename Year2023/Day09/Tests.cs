using NUnit.Framework;

namespace Day09;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 114)]
    public void Test_Runtime_SumOfExtrapolations(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfExtrapolations, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 2)]
    public void Test_Runtime_SumOfBackwardsExtrapolations(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfBackwardsExtrapolations, Is.EqualTo(expected));
    }

    [TestCase("0 3 6 9 12 15", 18)]
    [TestCase("1 3 6 10 15 21", 28)]
    [TestCase("10 13 16 21 30 45", 68)]
    public void Test_Reading_Extrapolation(string reading, int expected) {
        Assert.That(new Runtime.Reading(reading).extrapolation, Is.EqualTo(expected));
    }

    [TestCase("0 3 6 9 12 15", -3)]
    [TestCase("1 3 6 10 15 21", 0)]
    [TestCase("10 13 16 21 30 45", 5)]
    public void Test_Reading_BackwardsExtrapolation(string reading, int expected) {
        Assert.That(new Runtime.Reading(reading).backwardsExtrapolation, Is.EqualTo(expected));
    }
}