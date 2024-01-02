using NUnit.Framework;

namespace Day20;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 32000000)]
    [TestCase("example-2.txt", 11687500)]
    public void Test_Runtime(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.productOfPulses, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 5)]
    [TestCase("example-2.txt", 5)]
    public void Test_Module_Count(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.modules.Count, Is.EqualTo(expected));
    }
}