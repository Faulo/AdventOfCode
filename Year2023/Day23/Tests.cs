using NUnit.Framework;
using Utilities;

namespace Day23;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", false, 94)]
    [TestCase("input.txt", false, 2106)]
    [TestCase("example-1.txt", true, 154)]
    public void Test_Runtime_MaximumNumberOfSteps(string file, bool replaceSlopes, int expected) {
        var sut = new Runtime(file, replaceSlopes);

        Assert.That(sut.maximumNumberOfSteps, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 1, 0)]
    public void Test_Runtime_Start(string file, int x, int y) {
        var sut = new Runtime(file);

        var expected = new Vector2Int(x, y);

        Assert.That(sut.start, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 21, 22)]
    public void Test_Runtime_Goal(string file, int x, int y) {
        var sut = new Runtime(file);

        var expected = new Vector2Int(x, y);

        Assert.That(sut.goal, Is.EqualTo(expected));
    }

    [TestCase('.', 0, 1)]
    [TestCase('.', 0, -1)]
    [TestCase('.', 1, 0)]
    [TestCase('.', -1, 0)]
    [TestCase('^', 0, -1)]
    [TestCase('v', 0, 1)]
    [TestCase('<', -1, 0)]
    [TestCase('>', 1, 0)]
    public void Test_Runtime_GetNeighbors(char character, int x, int y) {
        Assert.That(character.GetNeighbors(), Contains.Item(new Vector2Int(x, y)));
    }
}