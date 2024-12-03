using NUnit.Framework;

namespace Day03;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 161)]
    public void Multiply(string file, int expected) {
        Assert.That(new Runtime(file).multSum, Is.EqualTo(expected));
    }
}