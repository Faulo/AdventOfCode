using NUnit.Framework;

namespace Day05;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 143)]
    public void Straight(string file, int expected) {
        Assert.That(new Runtime(file).sumOfMiddle, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, true)]
    [TestCase("example-1.txt", 1, true)]
    [TestCase("example-1.txt", 2, true)]
    [TestCase("example-1.txt", 3, false)]
    [TestCase("example-1.txt", 4, false)]
    [TestCase("example-1.txt", 5, false)]
    public void IsValid(string file, int index, bool expected) {
        var sut = new Runtime(file);
        Assert.That(sut.IsValid(sut.updates[index]), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 47, 53)]
    [TestCase("example-1.txt", 1, 97, 13)]
    public void Rule(string file, int index, int left, int right) {
        Assert.That(new Runtime(file).rules[index], Is.EqualTo((left, right)));
    }

    [TestCase("example-1.txt", 0, 75, 47, 61, 53, 29)]
    [TestCase("example-1.txt", 1, 97, 61, 53, 29, 13)]
    public void Update(string file, int index, params int[] update) {
        Assert.That(new Runtime(file).updates[index], Is.EqualTo(update));
    }
}