using NUnit.Framework;

namespace Day07;

class Tests {
    [TestCase("example.txt", 95437)]
    public void TotalSizeOfSmallDirectories(string file, int expected) {
        Assert.That(new Runtime(file).totalSizeOfSmallDirectories, Is.EqualTo(expected));
    }

    [TestCase("example.txt", 24933642)]
    public void TotalSizeOfDeletableDirectory(string file, int expected) {
        Assert.That(new Runtime(file).totalSizeOfDeletableDirectory, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 938745)]
    public void TotalSizeOfSmallDirectoriesIsGreaterThan(string file, int expected) {
        Assert.That(new Runtime(file).totalSizeOfSmallDirectories, Is.GreaterThan(expected));
    }

    [TestCase("example.txt", 48381165)]
    public void TotalSize(string file, int expected) {
        Assert.That(new Runtime(file).root.size, Is.EqualTo(expected));
    }
}
