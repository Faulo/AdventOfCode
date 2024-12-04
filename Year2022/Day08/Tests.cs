using NUnit.Framework;

namespace Day08;

class Tests {
    [TestCase("example.txt", 21)]
    public void TreeCount(string file, int expected) {
        Assert.That(new Runtime(file).visibleTreeCount, Is.EqualTo(expected));
    }
}
