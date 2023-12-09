using NUnit.Framework;

namespace Day08;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 2)]
    [TestCase("example-2.txt", 6)]
    public void Test_Runtime_NumberOfSteps(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.numberOfSteps, Is.EqualTo(expected));
    }

    [TestCase("example-3.txt", 6)]
    public void Test_Runtime_NumberOfGhostSteps(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.numberOfGhostSteps, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 1880032569870193489)]
    public void Test_Runtime_NumberOfGhostSteps_LessThan(string file, long expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.numberOfGhostSteps, Is.LessThan(expected));
    }

    [TestCase("example-1.txt", "RLRLRL")]
    [TestCase("example-2.txt", "LLRLLRLLRLLR")]
    public void Test_Runtime_InfiniteInstructions(string file, string expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.infiniteInstructions.Take(expected.Length), Is.EqualTo(Runtime.ParseDirections(expected)));
    }

    [TestCase("L", Runtime.Direction.Left)]
    [TestCase("R", Runtime.Direction.Right)]
    public void Test_Runtime_ParseDirections(string direction, Runtime.Direction expected) {
        Assert.That(Runtime.ParseDirections(direction), Is.EqualTo(new[] { expected }));
    }

    [Test]
    public void Test_Runtime_LeastCommonMultiple() {
        Assert.That(Runtime.LeastCommonMultiple(2, 3, 6), Is.EqualTo(6));
    }

    [TestCase("example-1.txt", "AAA")]
    [TestCase("example-1.txt", "ZZZ")]
    [TestCase("example-2.txt", "AAA")]
    [TestCase("example-2.txt", "BBB")]
    [TestCase("example-2.txt", "ZZZ")]
    public void Test_Runtime_Nodes(string file, string expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.nodes, Contains.Key(expected));
    }

    [TestCase]
    public void Test_Node_Direction() {
        var node = new Runtime.Node("A");
        var left = new Runtime.Node("B");
        var right = new Runtime.Node("C");

        node.left = left;
        node.right = right;

        Assert.That(node[Runtime.Direction.Left], Is.EqualTo(left));
        Assert.That(node[Runtime.Direction.Right], Is.EqualTo(right));
    }
}