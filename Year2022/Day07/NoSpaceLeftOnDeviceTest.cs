using NUnit.Framework;

namespace Day07;

class NoSpaceLeftOnDeviceTest {
    readonly NoSpaceLeftOnDevice program = new("example.txt");

    [Test]
    public void TestFindTotalSizeOfSmallDirectories() {
        Assert.That(program.FindTotalSizeOfSmallDirectories(), Is.EqualTo(95437));
    }
}
