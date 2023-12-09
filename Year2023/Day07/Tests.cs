using NUnit.Framework;

namespace Day07;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", false, 6440)]
    [TestCase("example-1.txt", true, 5905)]
    public void Test_Runtime_SumOfWinings(string file, bool useJoker, int notExpected) {
        var runtime = new Runtime(file, useJoker);

        Assert.That(runtime.sumOfWinnings, Is.EqualTo(notExpected));
    }
    [TestCase("input.txt", true, 253950034)]
    [TestCase("input.txt", true, 253945077)]
    public void Test_Runtime_SumOfWinings_LessThan(string file, bool useJoker, int notExpected) {
        var runtime = new Runtime(file, useJoker);

        Assert.That(runtime.sumOfWinnings, Is.LessThan(notExpected));
    }

    [TestCase("AAAAA", Runtime.Rank.FiveOfAKind)]
    [TestCase("AA8AA", Runtime.Rank.FourOfAKind)]
    [TestCase("23332", Runtime.Rank.FullHouse)]
    [TestCase("TTT98", Runtime.Rank.ThreeOfAKind)]
    [TestCase("23432", Runtime.Rank.TwoPair)]
    [TestCase("A23A4", Runtime.Rank.OnePair)]
    [TestCase("23456", Runtime.Rank.HighCard)]
    public void Test_Hand_Rank(string hand, Runtime.Rank expected) {
        Assert.That(new Runtime.Hand(hand).rank, Is.EqualTo(expected));
    }

    [TestCase("AJAAA", Runtime.Rank.FiveOfAKind)]
    [TestCase("QJJQ2", Runtime.Rank.FourOfAKind)]
    [TestCase("JKKK2", Runtime.Rank.FourOfAKind)]
    [TestCase("QQQQ2", Runtime.Rank.FourOfAKind)]
    [TestCase("T55J5", Runtime.Rank.FourOfAKind)]
    [TestCase("KTJJT", Runtime.Rank.FourOfAKind)]
    [TestCase("QQQJA", Runtime.Rank.FourOfAKind)]
    [TestCase("23J32", Runtime.Rank.FullHouse)]
    [TestCase("TTJ98", Runtime.Rank.ThreeOfAKind)]
    [TestCase("234J2", Runtime.Rank.ThreeOfAKind)]
    [TestCase("A23J4", Runtime.Rank.OnePair)]
    [TestCase("JJJJJ", Runtime.Rank.FiveOfAKind)]
    [TestCase("JJJJ2", Runtime.Rank.FiveOfAKind)]
    [TestCase("JJJ22", Runtime.Rank.FiveOfAKind)]
    [TestCase("JJ222", Runtime.Rank.FiveOfAKind)]
    [TestCase("J2222", Runtime.Rank.FiveOfAKind)]
    [TestCase("22222", Runtime.Rank.FiveOfAKind)]
    public void Test_Hand_Rank_WithJoker(string hand, Runtime.Rank expected) {
        Assert.That(new Runtime.Hand(hand, useJoker: true).rank, Is.EqualTo(expected));
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
    public void Test_CardComparer_NoJoker(char leftChar, char rightChar) {
        var comparer = new Runtime.CardComparer(false);

        Assert.That(comparer.Compare(leftChar, rightChar), Is.EqualTo(1));
        Assert.That(comparer.Compare(rightChar, leftChar), Is.EqualTo(-1));
    }

    [TestCase('A', 'K')]
    [TestCase('K', 'Q')]
    [TestCase('Q', 'J')]
    [TestCase('Q', 'T')]
    [TestCase('T', 'J')]
    [TestCase('T', '9')]
    [TestCase('9', '8')]
    [TestCase('8', '7')]
    [TestCase('7', '6')]
    [TestCase('6', '5')]
    [TestCase('5', '4')]
    [TestCase('4', '3')]
    [TestCase('3', '2')]
    [TestCase('2', 'J')]
    public void Test_CardComparer_Joker(char leftChar, char rightChar) {
        var comparer = new Runtime.CardComparer(true);

        Assert.That(comparer.Compare(leftChar, rightChar), Is.EqualTo(1));
        Assert.That(comparer.Compare(rightChar, leftChar), Is.EqualTo(-1));
    }
}