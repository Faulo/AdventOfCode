using NUnit.Framework;

namespace Day10;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 36)]
    [TestCase("input.txt", 786)]
    public void TrailheadCount(string file, long expected) {
        Assert.That(new Runtime(file).trailheadCount, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 81)]
    public void RatingCount(string file, long expected) {
        Assert.That(new Runtime(file).ratingCount, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1, 7, '0')]
    [TestCase("example-1.txt", 6, 6, '0')]
    public void CharacterAtPosition(string file, int x, int y, char expected) {
        Assert.That(new Runtime(file).map[(x, y)], Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1, 7, 5)]
    [TestCase("example-1.txt", 6, 6, 8)]
    public void RatingCountAtPosition(string file, int x, int y, int expected) {
        Assert.That(new Runtime(file).RatingCountForPosition(new(x, y)), Is.EqualTo(expected));
    }
}