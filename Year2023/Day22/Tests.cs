using NUnit.Framework;
using Utilities;

namespace Day22;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 5)]
    public void Test_Runtime_NumberOfSuperfluousBricks(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.numberOfSuperfluousBricks, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 674)]
    public void Test_Runtime_NumberOfSuperfluousBricks_LessThan(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.numberOfSuperfluousBricks, Is.LessThan(expected));
    }

    [TestCase("example-1.txt", 7)]
    public void Test_Runtime_SumOfFallingBricks2(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfFallingBricks, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 74448)]
    public void Test_Runtime_SumOfFallingBricks_LessThan(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfFallingBricks, Is.LessThan(expected));
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

    [TestCase("example-1.txt", 0, false)]
    [TestCase("example-1.txt", 1, true)]
    [TestCase("example-1.txt", 2, true)]
    [TestCase("example-1.txt", 3, true)]
    [TestCase("example-1.txt", 4, true)]
    [TestCase("example-1.txt", 5, false)]
    [TestCase("example-1.txt", 6, true)]
    public void Test_Runtime_IsSuperfluous(string file, int brick, bool expected) {
        var sut = new Runtime(file);

        Assert.That(sut.IsSuperfluous(sut.bricks[brick]), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 6)]
    [TestCase("example-1.txt", 1, 0)]
    [TestCase("example-1.txt", 5, 1)]
    public void Test_Runtime_GetFallingBricksCount(string file, int brick, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.GetFallingBricksCount(sut.bricks[brick]), Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, "1,2")]
    [TestCase("example-1.txt", 1, "3,4")]
    [TestCase("example-1.txt", 2, "3,4")]
    [TestCase("example-1.txt", 3, "5")]
    [TestCase("example-1.txt", 4, "5")]
    [TestCase("example-1.txt", 5, "6")]
    [TestCase("example-1.txt", 6, "")]
    public void Test_Runtime_Bricks_GetAbove(string file, int brick, string expectedBricks) {
        var sut = new Runtime(file);

        var actual = sut.GetNeighbors(sut.bricks[brick], Vector3Int.up);

        Assert.That(string.Join(',', actual.Select(b => sut.bricks.IndexOf(b))), Is.EqualTo(expectedBricks));
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