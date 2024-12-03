using NUnit.Framework;

namespace Day04;

class CampCleanupTest {
    [TestCase("2-4,6-8", 2, 4, 6, 8)]
    public void TestDivideIntoSecions(string line, int start1, int stop1, int start2, int stop2) {
        Assert.That(CampCleanup.DivideIntoSections(line), Is.EqualTo(((start1, stop1), (start2, stop2))));
    }

    [TestCase(2, 4, 6, 8, false)]
    [TestCase(2, 8, 6, 8, true)]
    [TestCase(6, 8, 2, 8, true)]
    public void TestDoSectionsOverlapCompletely(int start1, int stop1, int start2, int stop2, bool expected) {
        Assert.That(CampCleanup.DoSectionsOverlapCompletely((start1, stop1), (start2, stop2)), Is.EqualTo(expected));
    }

    [TestCase("example.txt", 2)]
    [TestCase("input.txt", 567)]
    public void TestSumOfCompletelyOverlappingSections(string file, int count) {
        Assert.That(CampCleanup.SumOfCompletelyOverlappingSections(file), Is.EqualTo(count));
    }

    [TestCase(2, 4, 6, 8, false)]
    [TestCase(2, 3, 4, 5, false)]
    [TestCase(2, 8, 3, 7, true)]
    [TestCase(5, 7, 7, 9, true)]
    [TestCase(2, 6, 4, 8, true)]
    [TestCase(6, 6, 4, 6, true)]
    public void TestDoSectionsOverlap(int start1, int stop1, int start2, int stop2, bool expected) {
        Assert.That(CampCleanup.DoSectionsOverlap((start1, stop1), (start2, stop2)), Is.EqualTo(expected));
    }

    [TestCase("example.txt", 4)]
    [TestCase("input.txt", 907)]
    public void TestSumOfOverlappingSections(string file, int count) {
        Assert.That(CampCleanup.SumOfOverlappingSections(file), Is.EqualTo(count));
    }
}
