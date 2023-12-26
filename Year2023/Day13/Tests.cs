using NUnit.Framework;

namespace Day13;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 405)]
    public void Test_Runtime_SumOfReflections(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfReflections, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 400)]
    public void Test_Runtime_SumOfReflectionsWithSmudgesFixed(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfReflectionsWithSmudgesFixed, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 39706)]
    public void Test_Runtime_SumOfReflectionsWithSmudgesFixed_Wrong(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfReflectionsWithSmudgesFixed, Is.LessThan(expected));
    }

    [TestCase("example-1.txt", 2)]
    public void Test_Runtime_Maps_Count(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps.Count, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 7)]
    [TestCase("example-1.txt", 1, 7)]
    public void Test_Runtime_Maps_RowCount(string file, int map, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps[map].rows.Count, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 0, "#.##..##.")]
    [TestCase("example-1.txt", 0, 6, "#.#.##.#.")]
    [TestCase("example-1.txt", 1, 0, "#...##..#")]
    [TestCase("example-1.txt", 1, 6, "#....#..#")]
    public void Test_Runtime_Maps_Rows(string file, int map, int row, string expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps[map].rows[row], Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 5)]
    [TestCase("example-1.txt", 1, 400)]
    public void Test_Runtime_Maps_Reflection(string file, int map, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps[map].reflection, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", 0, 300)]
    [TestCase("example-1.txt", 1, 100)]
    public void Test_Runtime_Maps_ReflectionWithSmudgesFixed(string file, int map, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.maps[map].reflectionWithSmudgesFixed, Is.EqualTo(expected));
    }
}