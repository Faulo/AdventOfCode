using NUnit.Framework;

namespace Day08;

class Tests {
    [TestCase("example.txt", 21)]
    [TestCase("input.txt", 1832)]
    public void TreeCount(string file, int expected) {
        Assert.That(new Runtime(file).visibleTreeCount, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 8)]
    public void ScenicScore(string file, int expected) {
        Assert.That(new Runtime(file).highestScenicScore, Is.EqualTo(expected));
    }
}
