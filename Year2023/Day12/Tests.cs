using NUnit.Framework;

namespace Day12;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 1, 21)]
    [TestCase("example-1.txt", 5, 525152)]
    [TestCase("input.txt", 1, 7090)]
    [TestCase("input.txt", 5, 6792010726878)]
    public void Test_Runtime_SumOfArrangements(string file, int foldCount, long expected) {
        var runtime = new Runtime(file, foldCount);

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

    [Test]
    public void Test_Record_FoldCount() {
        var record = Record.Parse("???.### 1,1,3", 5);

        Assert.That(record.springs, Is.EqualTo("???.###????.###????.###????.###????.###".ToCharArray()));
        Assert.That(record.damagedCounts, Is.EqualTo(new[] { 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 3 }));
    }

    [TestCase("???.### 1,1,3", 1, 1)]
    [TestCase("???.### 1,1,3", 5, 1)]
    [TestCase(".??..??...?##. 1,1,3", 1, 4)]
    [TestCase(".??..??...?##. 1,1,3", 5, 16384)]
    [TestCase("?#?#?#?#?#?#?#? 1,3,1,6", 1, 1)]
    [TestCase("?#?#?#?#?#?#?#? 1,3,1,6", 5, 1)]
    [TestCase("????.#...#... 4,1,1", 1, 1)]
    [TestCase("????.#...#... 4,1,1", 5, 16)]
    [TestCase("????.######..#####. 1,6,5", 1, 4)]
    [TestCase("????.######..#####. 1,6,5", 5, 2500)]
    [TestCase("?###???????? 3,2,1", 1, 10)]
    [TestCase("?###???????? 3,2,1", 5, 506250)]
    public void Test_Record_NumberOfArrangements(string record, int foldCount, int expected) {
        var sut = Record.Parse(record, foldCount);

        Assert.That(sut.numberOfArrangements, Is.EqualTo(expected));
    }

    [TestCase("???.### 1,1,3", 1, 0, "2,3,4")]
    [TestCase("???.### 1,1,3", 2, 0, "3,4")]
    [TestCase("???.### 1,1,3", 3, 0, "4,8")]
    [TestCase("???.### 1,1,3", 3, 4, "8")]
    [TestCase("???.### 1,1,3", 4, 0, "")]
    [TestCase("????.######..#####. 1,6,5", 1, 0, "2,3,4,5")]
    [TestCase("????.######..#####. 1,6,5", 6, 0, "12")]
    [TestCase("????.######..#####. 1,6,5", 6, 1, "12")]
    [TestCase("????.######..#####. 1,6,5", 6, 2, "12")]
    [TestCase("????.######..#####. 1,6,5", 6, 3, "12")]
    [TestCase("????.######..#####. 1,6,5", 6, 4, "12")]
    [TestCase("????.######..#####. 1,6,5", 6, 5, "12")]
    [TestCase("????.######..#####. 1,6,5", 5, 12, "19")]
    public void Test_Record_FindDamagedCount(string record, int damageCount, int start, string expected) {
        var sut = Record.Parse(record);

        Assert.That(
            sut.FindDamagedCount(damageCount, start),
            Is.EqualTo(string.IsNullOrEmpty(expected) ? Enumerable.Empty<int>() : expected.Split(',').Select(int.Parse))
        );
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