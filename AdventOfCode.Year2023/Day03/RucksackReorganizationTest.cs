using NUnit.Framework;

namespace Day03;

class RucksackReorganizationTest {
    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "vJrwpWtwJgWr", "hcsFMMfFFhFp")]
    public void TestDivideIntoCompartments(string line, string firstCompartment, string secondCompartment) {
        Assert.AreEqual((firstCompartment, secondCompartment), RucksackReorganization.DivideIntoCompartments(line));
    }

    [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", 'p')]
    public void TestFindLetterThatAppearsInBothCompartments(string line, char letterThatAppearsInBoth) {
        Assert.AreEqual(letterThatAppearsInBoth, RucksackReorganization.FindLetterThatAppearsInBothCompartments(line));
    }
}
