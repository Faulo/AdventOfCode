using NUnit.Framework;

namespace Day09;

[TestFixture(TestOf = typeof(Runtime))]
sealed class Tests {
    [TestCase("example-1.txt", 1928)]
    public void DefragChecksum(string file, long expected) {
        Assert.That(new Runtime(file).defragChecksum, Is.EqualTo(expected));
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

    [TestCase("12345", 0 + 3 + 4 + 5 + 20 + 22 + 24 + 26 + 28)]
    public void TestChecksum(string manifest, long expected) {
        Assert.That(new Runtime.HDD(manifest).checksum, Is.EqualTo(expected));
    }
}