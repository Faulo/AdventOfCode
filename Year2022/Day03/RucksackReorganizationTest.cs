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

    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg", 'r')]
    [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", 'Z')]
    public void TestFindGroupLetter(string first, string second, string third, char letter) {
        Assert.AreEqual(letter, RucksackReorganization.FindGroupLetter(first, second, third));
    }

    [TestCase("example.txt", 2)]
    public void TestDivideIntoGroups(string file, int count) {
        Assert.AreEqual(count, RucksackReorganization.DivideIntoGroups(file).Count());
    }

    [TestCase("example.txt", 70)]
    public void TestSumOfPriorityOfGroup(string file, int sum) {
        Assert.AreEqual(sum, RucksackReorganization.SumOfPriorityOfGroup(file));
    }
}
