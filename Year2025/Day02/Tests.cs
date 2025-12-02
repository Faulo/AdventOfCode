using NUnit.Framework;

namespace Day02;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 1227775554)]
    public void Test_sumOfInvalidIds(string file, long expected) {
        Assert.That(new Runtime(file).sumOfInvalidIds, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 11, 22)]
    [TestCase("example.txt", 2121212118, 2121212124)]
    public void Test_ranges(string file, long first, long last) {
        Assert.That(new Runtime(file).ranges, Does.Contain((first, last)));
    }

    [TestCase(11, 22, 11, 22)]
    [TestCase(95, 115, 99)]
    [TestCase(998, 1012, 1010)]
    public void Test_FindInvalidIds(long first, long last, params long[] invalidIds) {
        Assert.That(Runtime.FindInvalidIds(first, last).ToArray(), Is.EqualTo(invalidIds));
    }
}