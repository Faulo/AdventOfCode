using NUnit.Framework;

namespace Day05;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 35)]
    [TestCase("input.txt", 88151870)]
    public void TestLowestLocation(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.lowestLocation, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 46)]
    [TestCase("input.txt", 2008785)]
    public void TestLowestLocationOfPairs(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.lowestLocationOfPairs, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 79)]
    [TestCase("example-1.txt", 14)]
    [TestCase("example-1.txt", 55)]
    [TestCase("example-1.txt", 13)]
    public void TestSeeds(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.seeds, Contains.Item(expected));
    }

    [TestCase("input.txt")]
    public void TestSeeds(string file) {
        var runtime = new Runtime(file);

        Assert.That(runtime.pairsOfSeeds.Count(), Is.Not.EqualTo(0));
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
    public void GivenMap_123_WhenTryTranslate_ThenSucceed(long input, long expected) {
        var map = new Runtime.Map(1, 2, 3);

        Assert.That(map.CanTranslate(input), Is.True);
        Assert.That(map.Translate(input), Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(6)]
    [TestCase(5)]
    public void GivenMap_123_WhenTryTranslate_ThenFail(long input) {
        var map = new Runtime.Map(1, 2, 3);

        Assert.That(map.CanTranslate(input), Is.False);
    }

    [TestCase(2, 3, 1)]
    [TestCase(2, 4, 2)]
    public void GivenMap_123_WhenTranslatePair_ThenExpectCount(long start, long count, long expected) {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((start, count));

        Assert.That(actual.Count(), Is.EqualTo(expected));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_23_ThenExpectPair_13True() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((2, 3));

        Assert.That(actual, Contains.Item(((1, 3), true)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_24_ThenExpectPair_13True() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((2, 4));

        Assert.That(actual, Contains.Item(((1, 3), true)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_24_ThenExpectPair_51False() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((2, 4));

        Assert.That(actual, Contains.Item(((5, 1), false)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_03_ThenExpectPair_01False() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((0, 3));

        Assert.That(actual, Contains.Item(((0, 2), false)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_03_ThenExpectPair_12True() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((0, 3));

        Assert.That(actual, Contains.Item(((1, 1), true)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_53_ThenExpectPair_53False() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((5, 3));

        Assert.That(actual, Contains.Item(((5, 3), false)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_43_ThenExpectPair_31True() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((4, 3));

        Assert.That(actual, Contains.Item(((3, 1), true)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_43_ThenExpectPair_52False() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((4, 3));

        Assert.That(actual, Contains.Item(((5, 2), false)));
    }

    [Test]
    public void GivenMap_123_WhenTranslatePair_98_ThenExpectPair_98False() {
        var map = new Runtime.Map(1, 2, 3);

        var actual = map.TranslatePair((9, 8));

        Assert.That(actual, Contains.Item(((9, 8), false)));
    }

    [Test]
    public void GivenMap_198_WhenTranslatePair_12_ThenExpectPair_12False() {
        var map = new Runtime.Map(1, 9, 8);

        var actual = map.TranslatePair((1, 2));

        Assert.That(actual, Contains.Item(((1, 2), false)));
    }
}