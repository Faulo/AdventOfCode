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
    public void TestSumOfOverlappingSections(string file, int count) {
        Assert.AreEqual(count, CampCleanup.SumOfOverlappingSections(file));
    }
}
