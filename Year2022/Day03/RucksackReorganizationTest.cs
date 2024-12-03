using NUnit.Framework;

namespace Day03;

class RucksackReorganizationTest {
    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "vJrwpWtwJgWr", "hcsFMMfFFhFp")]
    [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "jqHRNqRjqzjGDLGL", "rsFMfFZSrLrFZsSL")]
    [TestCase("PmmdzqPrVvPwwTWBwg", "PmmdzqPrV", "vPwwTWBwg")]
    public void TestDivideIntoCompartments(string line, string firstCompartment, string secondCompartment) {
        Assert.That(RucksackReorganization.DivideIntoCompartments(line), Is.EqualTo((firstCompartment, secondCompartment)));
    }

    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", 'p')]
    [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", 'L')]
    [TestCase("PmmdzqPrVvPwwTWBwg", 'P')]
    public void TestFindLetterThatAppearsInBothCompartments(string line, char letterThatAppearsInBoth) {
        Assert.That(RucksackReorganization.FindLetterThatAppearsInBothCompartments(line), Is.EqualTo(letterThatAppearsInBoth));
    }

    [TestCase('a', 1)]
    [TestCase('z', 26)]
    [TestCase('A', 27)]
    [TestCase('Z', 52)]
    public void TestGetLetterPriority(char letter, int priority) {
        Assert.That(RucksackReorganization.GetLetterPriority(letter), Is.EqualTo(priority));
    }

    [TestCase("example.txt", 157)]
    [TestCase("input.txt", 7785)]
    public void TestSumOfPriorityOfLettersThatAppearInBothCompartmentsOfFile(string file, int sum) {
        Assert.That(RucksackReorganization.SumOfPriorityOfLettersThatAppearInBothCompartmentsOfFile(file), Is.EqualTo(sum));
    }

    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg", 'r')]
    [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", 'Z')]
    public void TestFindGroupLetter(string first, string second, string third, char letter) {
        Assert.That(RucksackReorganization.FindGroupLetter(first, second, third), Is.EqualTo(letter));
    }

    [TestCase("example.txt", 2)]
    public void TestDivideIntoGroups(string file, int count) {
        Assert.That(RucksackReorganization.DivideIntoGroups(file).Count(), Is.EqualTo(count));
    }

    [TestCase("example.txt", 70)]
    [TestCase("input.txt", 2633)]
    public void TestSumOfPriorityOfGroup(string file, int sum) {
        Assert.That(RucksackReorganization.SumOfPriorityOfGroup(file), Is.EqualTo(sum));
    }
}
