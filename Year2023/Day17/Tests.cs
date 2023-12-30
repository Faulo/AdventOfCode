using NUnit.Framework;

namespace Day17;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", false, 102)]
    [TestCase("example-1.txt", true, 94)]
    [TestCase("example-2.txt", false, 11)]
    [TestCase("example-3.txt", true, 71)]
    public void Test_Runtime_MininumHeatLoss(string file, bool isUltra, int expected) {
        var sut = new Runtime(file, isUltra);

        Assert.That(sut.mininumHeatLoss, Is.EqualTo(expected));
    }

    [TestCase("input.txt", true, 1196)]
    public void Test_Runtime_MininumHeatLoss_LessThan(string file, bool isUltra, int expected) {
        var sut = new Runtime(file, isUltra);

        Assert.That(sut.mininumHeatLoss, Is.LessThan(expected));
    }

    [Test]
    public void Test_Node_DirectionCount() {
        var first = new Node(default, new(0, 0), Directions.Up, 0);
        var second = new Node(first, new(0, 0), Directions.Up, 0);
        var third = new Node(second, new(0, 0), Directions.Up, 0);

        Assert.That(first.GetDirectionCount(Directions.Up), Is.EqualTo(1));
        Assert.That(second.GetDirectionCount(Directions.Up), Is.EqualTo(2));
        Assert.That(third.GetDirectionCount(Directions.Up), Is.EqualTo(3));

        Assert.That(first.GetDirectionCount(Directions.Down), Is.EqualTo(0));
        Assert.That(second.GetDirectionCount(Directions.Down), Is.EqualTo(0));
        Assert.That(third.GetDirectionCount(Directions.Down), Is.EqualTo(0));
    }

    [TestCase(0, 0, Directions.None, "o(0, 0)")]
    [TestCase(1, 1, Directions.Right, ">(1, 1)")]
    public void Test_Node_ToString(int x, int y, Directions direction, string expected) {
        var node = new Node(default, new(x, y), direction, 0);
        Assert.That(node.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void Test_Nodes_ToString() {
        var node = new Node(Node.empty, new(1, 0), Directions.Right, 0);
        Assert.That(node.ToString(), Is.EqualTo("o(0, 0)>(1, 0)"));
    }

    [Test]
    public void Test_Nodes_Equals() {
        var set = new HashSet<Node> {
            new(Node.empty, new(1, 0), Directions.Right, 0),
            new(Node.empty, new(1, 0), Directions.Right, 0),
            new(Node.empty, new(1, 0), Directions.Up, 0),
            new(Node.empty, new(1, 0), Directions.Up, 0),
            new(Node.empty, new(0, 1), Directions.Right, 0),
            new(Node.empty, new(0, 1), Directions.Right, 0),
        };

        Assert.That(set.Count, Is.EqualTo(3));
    }
}