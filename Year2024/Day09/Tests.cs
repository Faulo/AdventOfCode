using NUnit.Framework;

namespace Day09;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 1928)]
    [TestCase("input.txt", 6332189866718)]
    public void DefragChecksum(string file, long expected) {
        Assert.That(new Runtime(file).defragChecksum, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 2858)]
    public void SmartDefragChecksum(string file, long expected) {
        Assert.That(new Runtime(file).smartDefragChecksum, Is.EqualTo(expected));
    }

    [TestCase("12345", 0, 1, -1, 2, 1, 3, -1, 4, 2, 5)]
    public void Blocks(string manifest, params int[] blocks) {
        var expected = new List<Runtime.Block>();
        for (int i = 0; i < blocks.Length; i += 2) {
            expected.Add(new(blocks[i], blocks[i + 1]));
        }

        Assert.That(new Runtime.HDD(manifest).blocks, Is.EqualTo(expected));
    }

    [TestCase("12345", 0, 1, 2, 2, 1, 3, 2, 3)]
    public void Defrag(string manifest, params int[] blocks) {
        var expected = new List<Runtime.Block>();
        for (int i = 0; i < blocks.Length; i += 2) {
            expected.Add(new(blocks[i], blocks[i + 1]));
        }

        var sut = new Runtime.HDD(manifest);

        sut.Defrag();

        Assert.That(sut.blocks, Is.EqualTo(expected));
    }

    [TestCase("2333133121414131402", 0, 2, 9, 2, 2, 1, 1, 3, 7, 3, -1, 1, 4, 2, -1, 1, 3, 3, -1, 4, 5, 4, -1, 1, 6, 4, -1, 5, 8, 4, -1, 2)]
    public void SmartDefrag(string manifest, params int[] blocks) {
        var expected = new List<Runtime.Block>();
        for (int i = 0; i < blocks.Length; i += 2) {
            expected.Add(new(blocks[i], blocks[i + 1]));
        }

        var sut = new Runtime.HDD(manifest);

        sut.SmartDefrag();

        Assert.That(sut.blocks, Is.EqualTo(expected));
    }

    [TestCase("12345", 0 + 3 + 4 + 5 + 20 + 22 + 24 + 26 + 28)]
    public void TestChecksum(string manifest, long expected) {
        Assert.That(new Runtime.HDD(manifest).checksum, Is.EqualTo(expected));
    }
}