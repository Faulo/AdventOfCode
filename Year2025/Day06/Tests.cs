using NUnit.Framework;

namespace Day06;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example.txt", 4277556, true)]
    [TestCase("example.txt", 3263827, false)]
    [TestCase("input.txt", 8108520669952, true)]
    [TestCase("input.txt", 11708563470209, false)]
    public void Test_grandTotal(string file, long expected, bool parseHorizontally) {
        Assert.That(new Runtime(file, parseHorizontally).grandTotal, Is.EqualTo(expected));
    }
}