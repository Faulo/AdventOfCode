using NUnit.Framework;

namespace Day06;

class TuningTroubleTest {
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", MessageType.StartOfPacket, 5)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", MessageType.StartOfMessage, 23)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", MessageType.StartOfPacket, 6)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", MessageType.StartOfMessage, 23)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", MessageType.StartOfPacket, 10)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", MessageType.StartOfMessage, 29)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", MessageType.StartOfPacket, 11)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", MessageType.StartOfMessage, 26)]
    public void TestFindStart(string data, MessageType type, int expected) {
        Assert.AreEqual(expected, TuningTrouble.FindStart(data.ToCharArray(), type));
    }

    [TestCase("example.txt", "mjqjpqmgbljsphdztnvjfqwrcgsmlb")]
    public void TestParseMoves(string file, string data) {
        Assert.AreEqual(data.ToCharArray(), new TuningTrouble(file).ReadFile());
    }

    [TestCase("example.txt", MessageType.StartOfPacket, 7)]
    [TestCase("example.txt", MessageType.StartOfMessage, 19)]
    [TestCase("input.txt", MessageType.StartOfPacket, 1262)]
    [TestCase("input.txt", MessageType.StartOfMessage, 3444)]
    public void TestFindStartInFile(string file, MessageType type, int expected) {
        Assert.AreEqual(expected, new TuningTrouble(file).FindStartInFile(type));
    }
}
