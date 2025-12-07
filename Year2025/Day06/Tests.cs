using NUnit.Framework;

namespace Day06;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 4277556)]
    [TestCase("input.txt", 8108520669952)]
    public void Test_grandTotal(string file, long expected) {
        Assert.That(new Runtime(file).grandTotal, Is.EqualTo(expected));
    }
}