using NUnit.Framework;

namespace Day03;

class RucksackReorganizationTest {
    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "vJrwpWtwJgWr", "hcsFMMfFFhFp")]
    [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "jqHRNqRjqzjGDLGL", "rsFMfFZSrLrFZsSL")]
    [TestCase("PmmdzqPrVvPwwTWBwg", "PmmdzqPrV", "vPwwTWBwg")]
    public void TestDivideIntoCompartments(string line, string firstCompartment, string secondCompartment) {
        Assert.AreEqual((firstCompartment, secondCompartment), RucksackReorganization.DivideIntoCompartments(line));
    }

    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", 'p')]
    [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 'L')]
    [TestCase("PmmdzqPrVvPwwTWBwg", 'P')]
    public void TestFindLetterThatAppearsInBothCompartments(string line, char letterThatAppearsInBoth) {
        Assert.AreEqual(letterThatAppearsInBoth, RucksackReorganization.FindLetterThatAppearsInBothCompartments(line));
    }

    [TestCase('a', 1)]
    [TestCase('z', 26)]
    [TestCase('A', 27)]
    [TestCase('Z', 52)]
    public void TestGetLetterPriority(char letter, int priority) {
        Assert.AreEqual(priority, RucksackReorganization.GetLetterPriority(letter));
    }

    [TestCase("example.txt", 157)]
    public void TestSumOfPriorityOfLettersThatAppearInBothCompartmentsOfFile(string file, int sum) {
        Assert.AreEqual(sum, RucksackReorganization.SumOfPriorityOfLettersThatAppearInBothCompartmentsOfFile(file));
    }
}
