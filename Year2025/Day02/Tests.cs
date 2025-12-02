using NUnit.Framework;

namespace Day02;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 1227775554)]
    public void Test_sumOfInvalidIds(string file, long expected) {
        Assert.That(new Runtime(file).sumOfInvalidIds, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 4174379265)]
    public void Test_sumOfAllInvalidIds(string file, long expected) {
        Assert.That(new Runtime(file).sumOfAllInvalidIds, Is.EqualTo(expected));
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

    [TestCase(11, 22, 11, 22)]
    [TestCase(95, 115, 99, 111)]
    [TestCase(998, 1012, 999, 1010)]
    [TestCase(1188511880, 1188511890, 1188511885)]
    [TestCase(222220, 222224, 222222)]
    [TestCase(1698522, 1698528)]
    [TestCase(446443, 446449, 446446)]
    [TestCase(38593856, 38593862, 38593859)]
    [TestCase(565653, 565659, 565656)]
    [TestCase(824824821, 824824827, 824824824)]
    [TestCase(2121212118, 2121212124, 2121212121)]
    public void Test_FindAllInvalidIds(long first, long last, params long[] invalidIds) {
        Assert.That(Runtime.FindAllInvalidIds(first, last).ToArray(), Is.EqualTo(invalidIds));
    }
}