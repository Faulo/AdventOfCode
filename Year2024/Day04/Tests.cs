using NUnit.Framework;

namespace Day04;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 18)]
    public void Multiply(string file, int expected) {
        Assert.That(new Runtime(file).occurences, Is.EqualTo(expected));
    }
}