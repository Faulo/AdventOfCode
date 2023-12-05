using NUnit.Framework;

namespace Day05;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 35)]
    public void TestCalculateSumOfWins(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.lowestLocation, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 79)]
    [TestCase("example-1.txt", 14)]
    [TestCase("example-1.txt", 55)]
    [TestCase("example-1.txt", 13)]
    public void TestSeeds(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.seeds, Contains.Item(expected));
    }

    [TestCase("example-1.txt", 79, 82)]
    [TestCase("example-1.txt", 14, 43)]
    [TestCase("example-1.txt", 55, 86)]
    [TestCase("example-1.txt", 13, 35)]
    public void TestGetSeedLocation(string file, long seed, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.Translate(seed), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", "seed-to-soil", 50, 98, 2)]
    [TestCase("example-1.txt", "seed-to-soil", 52, 50, 48)]
    [TestCase("example-1.txt", "soil-to-fertilizer", 37, 52, 2)]
    [TestCase("example-1.txt", "water-to-light", 88, 18, 7)]
    public void TestGetSeedMap(string file, string map, long destination, long start, long count) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps, Contains.Key(map));
        Assert.That(runtime.maps[map], Contains.Item(new Runtime.Map(destination, start, count)));
    }

    [TestCase(2, 1)]
    [TestCase(3, 2)]
    [TestCase(4, 3)]
    public void GivenMap123_WhenTryTranslate_ThenSucceed(long input, long expected) {
        var map = new Runtime.Map(1, 2, 3);

        Assert.That(map.TryTranslate(ref input), Is.True);
        Assert.That(input, Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(6)]
    [TestCase(5)]
    public void GivenMap123_WhenTryTranslate_ThenFail(long input) {
        long expected = input;

        var map = new Runtime.Map(1, 2, 3);

        Assert.That(map.TryTranslate(ref input), Is.False);
        Assert.That(input, Is.EqualTo(input));
    }
}