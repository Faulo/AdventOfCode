using NUnit.Framework;

namespace Day08;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", false, 288)]
    [TestCase("example-2.txt", false, 71503)]
    [TestCase("example-1.txt", true, 71503)]
    public void Test_Runtime_ProductOfWins(string file, bool badKerning, int expected) {
        var runtime = new Runtime(file, badKerning);

        Assert.That(runtime.productOfWins, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 7, 9)]
    [TestCase("example-1.txt", 15, 40)]
    [TestCase("example-1.txt", 30, 200)]
    public void Test_Runtime_Races(string file, int time, int distance) {
        var runtime = new Runtime(file);

        Assert.That(runtime.races, Contains.Item(new Runtime.Race(time, distance)));
    }

    [TestCase(7, 9, 4)]
    [TestCase(15, 40, 8)]
    [TestCase(30, 200, 9)]
    public void Test_Race_Wins(int time, int distance, int expected) {
        var race = new Runtime.Race(time, distance);

        Assert.That(race.wins, Is.EqualTo(expected));
    }

    [TestCase(15, 40, 4)]
    public void Test_Race_FirstWin(int time, int distance, int expected) {
        var race = new Runtime.Race(time, distance);

        Assert.That(race.firstWin, Is.EqualTo(expected));
    }

    [TestCase(15, 40, 11)]
    public void Test_Race_LastWin(int time, int distance, int expected) {
        var race = new Runtime.Race(time, distance);

        Assert.That(race.lastWin, Is.EqualTo(expected));
    }
}