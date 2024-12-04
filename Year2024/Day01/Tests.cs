using NUnit.Framework;
using Year2024.Day01;

namespace Day01;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 11)]
    [TestCase("input.txt", 1603498)]
    public void TotalDistance(string file, int expected) {
        Assert.That(new Runtime(file).totalDistance, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 31)]
    [TestCase("input.txt", 25574739)]
    public void SimilarityScore(string file, int expected) {
        Assert.That(new Runtime(file).similarityScore, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 3, 4, 2, 1, 3, 3)]
    public void LeftColumn(string file, params int[] expected) {
        Assert.That(new Runtime(file).left, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 4, 3, 5, 3, 9, 3)]
    public void RightColumn(string file, params int[] expected) {
        Assert.That(new Runtime(file).right, Is.EqualTo(expected));
    }

    [TestCase(3, 4, 1)]
    [TestCase(7, 2, 5)]
    public void Delta(int left, int right, int expected) {
        Assert.That(Runtime.Delta(left, right), Is.EqualTo(expected));
    }
}