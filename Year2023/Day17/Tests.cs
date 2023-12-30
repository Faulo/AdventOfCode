using NUnit.Framework;

namespace Day17;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 102)]
    [TestCase("example-2.txt", 11)]
    public void Test_Runtime_MininumHeatLoss(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.mininumHeatLoss, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Node_DirectionCount() {
        var first = new Node(default, new(0, 0), Directions.Up, 0);
        var second = new Node(first, new(0, 0), Directions.Up, 0);
        var third = new Node(second, new(0, 0), Directions.Up, 0);

        Assert.That(first.IsDirectionCount(Directions.Up, 1), Is.EqualTo(true));
        Assert.That(first.IsDirectionCount(Directions.Up, 2), Is.EqualTo(false));
        Assert.That(second.IsDirectionCount(Directions.Up, 1), Is.EqualTo(true));
        Assert.That(second.IsDirectionCount(Directions.Up, 2), Is.EqualTo(true));
        Assert.That(third.IsDirectionCount(Directions.Up, 2), Is.EqualTo(true));
        Assert.That(third.IsDirectionCount(Directions.Up, 3), Is.EqualTo(true));

        Assert.That(first.IsDirectionCount(Directions.Down, 1), Is.EqualTo(false));
        Assert.That(second.IsDirectionCount(Directions.Down, 1), Is.EqualTo(false));
        Assert.That(third.IsDirectionCount(Directions.Down, 1), Is.EqualTo(false));
    }
}