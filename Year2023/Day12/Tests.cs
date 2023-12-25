using NUnit.Framework;

namespace Day12;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 21)]
    public void Test_Runtime_SumOfArrangements(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfArrangements, Is.EqualTo(expected));
    }

    [TestCase("example-1.txt", "???.### 1,1,3")]
    [TestCase("example-1.txt", ".??..??...?##. 1,1,3")]
    [TestCase("example-1.txt", "?#?#?#?#?#?#?#? 1,3,1,6")]
    [TestCase("example-1.txt", "????.#...#... 4,1,1")]
    [TestCase("example-1.txt", "????.######..#####. 1,6,5")]
    [TestCase("example-1.txt", "?###???????? 3,2,1")]
    public void Test_Runtime_Records(string file, string record) {
        var runtime = new Runtime(file);

        Assert.That(runtime.records, Contains.Item(Record.Parse(record)));
    }

    [Test]
    public void Test_Record_Parse() {
        var record = Record.Parse("???.### 1,1,3");

        Assert.That(record.springs, Is.EqualTo("???.###".ToCharArray()));
        Assert.That(record.damagedCounts, Is.EqualTo(new[] { 1, 1, 3 }));
    }

    [TestCase("???.### 1,1,3", 1)]
    [TestCase(".??..??...?##. 1,1,3", 4)]
    [TestCase("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [TestCase("????.#...#... 4,1,1", 1)]
    [TestCase("????.######..#####. 1,6,5", 4)]
    [TestCase("?###???????? 3,2,1", 10)]
    public void Test_Record_NumberOfArrangements(string record, int expected) {
        var sut = Record.Parse(record);

        Assert.That(sut.numberOfArrangements, Is.EqualTo(expected));
    }

    [TestCase("#.#.###", "1, 1, 3")]
    [TestCase(".#...#....###.", "1,1,3")]
    [TestCase(".#.###.#.######", "1,3,1,6")]
    [TestCase("####.#...#...", "4,1,1")]
    [TestCase("#....######..#####.", "1,6,5")]
    [TestCase(".###.##....#", "3,2,1")]
    public void Test_MatchesCount(string springs, string damagedCounts) {
        Assert.That(springs.ToCharArray().MatchesCount(damagedCounts.Split(',').Select(int.Parse).ToArray(), new char[springs.Length]), Is.True);
    }
}