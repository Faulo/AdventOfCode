using NUnit.Framework;

namespace Day02;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 8)]
    public void Given12OfEachCube_WhenCalculatePossible_ThenReturn8(string file, int expected) {
        var runtime = new Runtime();

        Assert.That(runtime.CalculatePossible(file), Is.EqualTo(expected));
    }

    [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1)]
    [TestCase("Game 123: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 123)]
    public void GivenLine_WhenParse_ThenReturnInt(string line, int expected) {
        var (id, _) = Runtime.ParseLine(line);

        Assert.That(id, Is.EqualTo(expected));
    }

    [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 6)]
    [TestCase("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 15)]
    public void GivenLine_WhenParse_ThenReturnBlues(string line, int expected) {
        var (_, cubes) = Runtime.ParseLine(line);

        Assert.That(cubes["blue"], Is.EqualTo(expected));
    }
}