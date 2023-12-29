using NUnit.Framework;

namespace Day16;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 46)]
    public void Test_Runtime_SumOfHashes(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.numberOfEnergizedTiles, Is.EqualTo(expected));
    }

    [TestCase('.', Directions.Up, Directions.Up)]
    [TestCase('-', Directions.Up, Directions.Left | Directions.Right)]
    [TestCase('-', Directions.Down, Directions.Left | Directions.Right)]
    [TestCase('-', Directions.Left, Directions.Left)]
    [TestCase('-', Directions.Right, Directions.Right)]
    [TestCase('|', Directions.Up, Directions.Up)]
    [TestCase('|', Directions.Down, Directions.Down)]
    [TestCase('|', Directions.Left, Directions.Up | Directions.Down)]
    [TestCase('|', Directions.Right, Directions.Up | Directions.Down)]
    [TestCase('\\', Directions.Up, Directions.Left)]
    [TestCase('\\', Directions.Down, Directions.Right)]
    [TestCase('\\', Directions.Left, Directions.Up)]
    [TestCase('\\', Directions.Right, Directions.Down)]
    [TestCase('/', Directions.Up, Directions.Right)]
    [TestCase('/', Directions.Down, Directions.Left)]
    [TestCase('/', Directions.Left, Directions.Down)]
    [TestCase('/', Directions.Right, Directions.Up)]
    public void Test_Extensions_GetDirections(char tile, Directions direction, Directions expected) {
        Assert.That(tile.GetDirections(direction), Is.EqualTo(expected));
    }
}