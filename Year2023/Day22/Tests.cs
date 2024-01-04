using NUnit.Framework;
using Utilities;

namespace Day22;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 5)]
    public void Test_Runtime(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.numberOfSuperfluousBricks, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 7)]
    public void Test_Runtime_Bricks_Count(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.bricks.Count, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 1)]
    [TestCase("example-1.txt", 1, 2)]
    [TestCase("example-1.txt", 2, 2)]
    [TestCase("example-1.txt", 3, 3)]
    [TestCase("example-1.txt", 4, 3)]
    [TestCase("example-1.txt", 5, 4)]
    [TestCase("example-1.txt", 6, 5)]
    public void Test_Runtime_Bricks_Z(string file, int brick, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.bricks[brick].from.z, Is.EqualTo(expected));
    }

    [TestCase("1,0,1~1,2,1", 1, 0, 1, 1, 2, 1)]
    [TestCase("1,1,8~1,1,9", 1, 1, 8, 1, 1, 9)]
    public void Test_Brick_Parse(string line, int x1, int y1, int z1, int x2, int y2, int z2) {
        var sut = Brick.Parse(line);

        Assert.That(sut.from, Is.EqualTo(new Vector3Int(x1, y1, z1)));
        Assert.That(sut.to, Is.EqualTo(new Vector3Int(x2, y2, z2)));
    }

    [TestCase("1,0,1~1,2,1", 1, 0, 1)]
    [TestCase("1,0,1~1,2,1", 1, 1, 1)]
    [TestCase("1,0,1~1,2,1", 1, 2, 1)]
    public void Test_Brick_Positions(string line, int x, int y, int z) {
        var sut = Brick.Parse(line);

        Assert.That(sut.positions, Contains.Item(new Vector3Int(x, y, z)));
    }
}