using NUnit.Framework;

namespace Day04;

class CampCleanupTest {
    [TestCase("2-4,6-8", 2, 4, 6, 8)]
    public void TestDivideIntoSecions(string line, int start1, int stop1, int start2, int stop2) {
        Assert.AreEqual(((start1, stop1), (start2, stop2)), CampCleanup.DivideIntoSections(line));
    }

    [TestCase(2, 4, 6, 8, false)]
    [TestCase(2, 8, 6, 8, true)]
    [TestCase(6, 8, 2, 8, true)]
    public void TestDoSectionsOverlapCompletely(int start1, int stop1, int start2, int stop2, bool expected) {
        Assert.AreEqual(expected, CampCleanup.DoSectionsOverlapCompletely((start1, stop1), (start2, stop2)));
    }

    [TestCase("example.txt", 2)]
    public void TestSumOfCompletelyOverlappingSections(string file, int count) {
        Assert.AreEqual(count, CampCleanup.SumOfCompletelyOverlappingSections(file));
    }

    [TestCase(2, 4, 6, 8, false)]
    [TestCase(2, 3, 4, 5, false)]
    [TestCase(2, 8, 3, 7, true)]
    [TestCase(5, 7, 7, 9, true)]
    [TestCase(2, 6, 4, 8, true)]
    [TestCase(6, 6, 4, 6, true)]
    public void TestDoSectionsOverlap(int start1, int stop1, int start2, int stop2, bool expected) {
        Assert.AreEqual(expected, CampCleanup.DoSectionsOverlap((start1, stop1), (start2, stop2)));
    }

    [TestCase("example.txt", 4)]
    public void TestSumOfOverlappingSections(string file, int count) {
        Assert.AreEqual(count, CampCleanup.SumOfOverlappingSections(file));
    }
}
