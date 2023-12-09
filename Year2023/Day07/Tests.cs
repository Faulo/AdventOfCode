using NUnit.Framework;

namespace Day07;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 6440)]
    public void Test_Runtime_SumOfWinings(string file, int expected) {
        var runtime = new Runtime(file);

        Assert.That(runtime.sumOfWinnings, Is.EqualTo(expected));
    }

    [TestCase("AAAAA", 7)] // five of a kind
    [TestCase("AA8AA", 6)] // four of a kind
    [TestCase("23332", 5)] // full house
    [TestCase("TTT98", 4)] // three of a kind
    [TestCase("23432", 3)] // two pair
    [TestCase("A23A4", 2)] // one pair
    [TestCase("23456", 1)] // high card
    public void Test_Hand_Rank(string hand, int expected) {
        Assert.That(new Runtime.Hand(hand).rank, Is.EqualTo(expected));
    }

    [TestCase("32T3K")]
    [TestCase("T55J5")]
    [TestCase("KK677")]
    [TestCase("KTJJT")]
    [TestCase("QQQJA")]
    public void Test_Hand_CompareTo_Same(string hand) {
        Assert.That(new Runtime.Hand(hand).CompareTo(new Runtime.Hand(hand)), Is.EqualTo(0));
    }

    [TestCase("32T3K", "T55J5")]
    [TestCase("32T3K", "KK677")]
    [TestCase("32T3K", "KTJJT")]
    [TestCase("32T3K", "QQQJA")]
    public void Test_Hand_CompareTo_Weaker(string leftHand, string rightHand) {
        Assert.That(new Runtime.Hand(leftHand).CompareTo(new Runtime.Hand(rightHand)), Is.EqualTo(-1));
    }

    [TestCase("T55J5", "32T3K")]
    [TestCase("KK677", "32T3K")]
    [TestCase("KTJJT", "32T3K")]
    [TestCase("QQQJA", "32T3K")]
    public void Test_Hand_CompareTo_ToStronger(string leftHand, string rightHand) {
        Assert.That(new Runtime.Hand(leftHand).CompareTo(new Runtime.Hand(rightHand)), Is.EqualTo(1));
    }

    [TestCase('A', 'K')]
    [TestCase('K', 'Q')]
    [TestCase('Q', 'J')]
    [TestCase('J', 'T')]
    [TestCase('T', '9')]
    [TestCase('9', '8')]
    [TestCase('8', '7')]
    [TestCase('7', '6')]
    [TestCase('6', '5')]
    [TestCase('5', '4')]
    [TestCase('4', '3')]
    [TestCase('3', '2')]
    [TestCase('A', '2')]
    public void Test_CardComparer(char leftChar, char rightChar) {
        var comparer = new Runtime.CardComparer();

        Assert.That(comparer.Compare(leftChar, rightChar), Is.EqualTo(1));
        Assert.That(comparer.Compare(rightChar, leftChar), Is.EqualTo(-1));
    }
}