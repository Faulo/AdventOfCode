using NUnit.Framework;

namespace Day05;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 3)]
    public void Test_freshIngredients(string file, int expected) {
        Assert.That(new Runtime(file).freshIngredients, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 14)]
    public void Test_allFreshIngredients(string file, int expected) {
        Assert.That(new Runtime(file).allFreshIngredients, Is.EqualTo(expected));
    }
}