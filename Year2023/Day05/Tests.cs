using NUnit.Framework;

namespace Day05;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 35)]
    public void TestCalculateSumOfWins(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.lowestLocation, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 79)]
    [TestCase("example-1.txt", 14)]
    [TestCase("example-1.txt", 55)]
    [TestCase("example-1.txt", 13)]
    public void TestSeeds(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.seeds, Contains.Item(expected));
    }

    [TestCase("example-1.txt", 79, 82)]
    [TestCase("example-1.txt", 14, 42)]
    [TestCase("example-1.txt", 55, 86)]
    [TestCase("example-1.txt", 13, 35)]
    public void TestGetSeedLocation(string file, int seed, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.GetSeedLocation(seed), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", "seed-to-soil", 50, 98, 2)]
    [TestCase("example-1.txt", "seed-to-soil", 52, 50, 48)]
    [TestCase("example-1.txt", "soil-to-fertilizer", 37, 52, 2)]
    [TestCase("example-1.txt", "water-to-light", 88, 18, 7)]
    public void TestGetSeedMap(string file, string map, int destination, int start, int count) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps, Contains.Key(map));
        Assert.That(runtime.maps[map], Contains.Item(new Runtime.Map(destination, start, count)));
    }
}