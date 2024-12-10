using NUnit.Framework;

namespace Day03;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 161)]
    [TestCase("input.txt", 174960292)]
    public void Multiply(string file, int expected) {
        Assert.That(new Runtime(file).multSum, Is.EqualTo(expected));
    }

    [TestCase("example-2.txt", 48)]
    [TestCase("input.txt", 56275602)]
    public void MultiplyWithDo(string file, int expected) {
        Assert.That(new Runtime(file, true).multSum, Is.EqualTo(expected));
    }
}