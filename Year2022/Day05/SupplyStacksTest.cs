using NUnit.Framework;

namespace Day05;

class SupplyStacksTest {
    [TestCase("example.txt", 3)]
    [TestCase("input.txt", 9)]
    public void TestParseStackCount(string file, int count) {
        Assert.AreEqual(count, new SupplyStacks(file).ParseStackCount());
    }
    [TestCase("example.txt", 3)]
    [TestCase("input.txt", 8)]
    public void TestParseStackMaxHeight(string file, int count) {
        Assert.AreEqual(count, new SupplyStacks(file).ParseStackMaxHeight());
    }

    [TestCase("example.txt")]
    public void TestParseStacksMatchesStackCount(string file) {
        Assert.AreEqual(new SupplyStacks(file).ParseStackCount(), new SupplyStacks(file).ParseStacks().Count);
    }

    [TestCase("example.txt", 1, "ZN")]
    [TestCase("example.txt", 2, "MCD")]
    [TestCase("example.txt", 3, "P")]
    public void TestParseStacks(string file, int index, string stack) {
        var stacks = new SupplyStacks(file).ParseStacks();

        CollectionAssert.AreEqual(new Stack<char>(stack.ToCharArray()), stacks[index]);
    }

    [TestCase("example.txt", 0, 1, 2, 1)]
    [TestCase("example.txt", 1, 3, 1, 3)]
    public void TestParseMoves(string file, int index, int amount, int source, int target) {
        var moves = new SupplyStacks(file).ParseMoves();

        Assert.AreEqual((amount, source, target), moves.ElementAt(index));
    }

    [Test]
    public void TestExecuteMove() {
        var stacks = new Dictionary<int, Stack<char>> {
            [2] = new(),
            [4] = new(new[] { 'A' })
        };

        SupplyStacks.Execute((1, 4, 2), stacks);

        CollectionAssert.AreEqual(new Stack<char>(new[] { 'A' }), stacks[2]);
        CollectionAssert.AreEqual(new Stack<char>(), stacks[4]);
    }

    [Test]
    public void TestExecuteMoves9000() {
        var stacks = new Dictionary<int, Stack<char>> {
            [2] = new(),
            [4] = new(new[] { 'A', 'B', 'C' })
        };

        SupplyStacks.Execute((2, 4, 2), stacks, Model.CrateMover9000);

        CollectionAssert.AreEqual(new Stack<char>(new[] { 'C', 'B' }), stacks[2]);
        CollectionAssert.AreEqual(new Stack<char>(new[] { 'A' }), stacks[4]);
    }

    [Test]
    public void TestExecuteMoves9001() {
        var stacks = new Dictionary<int, Stack<char>> {
            [2] = new(),
            [4] = new(new[] { 'A', 'B', 'C' })
        };

        SupplyStacks.Execute((2, 4, 2), stacks, Model.CrateMover9001);

        CollectionAssert.AreEqual(new Stack<char>(new[] { 'B', 'C' }), stacks[2]);
        CollectionAssert.AreEqual(new Stack<char>(new[] { 'A' }), stacks[4]);
    }

    [Test]
    public void TestPrintStacks() {
        var stacks = new Dictionary<int, Stack<char>> {
            [2] = new(),
            [4] = new(new[] { 'D', }),
            [6] = new(new[] { 'A', 'B', 'C' }),
            [8] = new(new[] { 'E' }),
        };

        Assert.AreEqual(" DCE", SupplyStacks.PrintStacks(stacks));
    }

    [TestCase("example.txt", 1, "DCP", Model.CrateMover9000)]
    public void TestExecuteExampleMovesAfter(string file, int moveCount, string expected, Model model) {
        Assert.AreEqual(expected, new SupplyStacks(file, model).ExecuteAndPrint(moveCount));
    }

    [TestCase("example.txt", "CMZ", Model.CrateMover9000)]
    [TestCase("example.txt", "MCD", Model.CrateMover9001)]
    public void TestExecuteExampleMoves(string file, string expected, Model model) {
        Assert.AreEqual(expected, new SupplyStacks(file, model).ExecuteAndPrint());
    }
}
