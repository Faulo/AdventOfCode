using NUnit.Framework;

namespace Day07;

class NoSpaceLeftOnDeviceTest {
    readonly NoSpaceLeftOnDevice program = new("example.txt");

    [Test]
    public void TestFindTotalSizeOfSmallDirectories() {
        //Assert.AreEqual(95437, program.FindTotalSizeOfSmallDirectories());
    }
}
