using NUnit.Framework;

namespace Day15;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 1320)]
    [TestCase("input.txt", 506891)]
    public void Test_Runtime_SumOfHashes(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfHashes, Is.EqualTo(expected));
    }

    [TestCase("input.txt", 506805)]
    public void Test_Runtime_SumOfHashes_GreaterThan(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfHashes, Is.GreaterThan(expected));
    }

    [TestCase("example-1.txt", 0, 0, "rn", 1)]
    [TestCase("example-1.txt", 0, 1, "cm", 2)]
    [TestCase("example-1.txt", 3, 0, "ot", 7)]
    [TestCase("example-1.txt", 3, 1, "ab", 5)]
    [TestCase("example-1.txt", 3, 2, "pc", 6)]
    public void Test_Runtime_Boxes(string file, byte boxIndex, int slotIndex, string expectedLabel, int expectedFocus) {
        var sut = new Runtime(file);
        var boxes = sut.boxes;

        Assert.That(boxes, Contains.Key(boxIndex));

        var box = boxes[boxIndex];

        Assert.That(box.Count, Is.GreaterThan(slotIndex));

        var slot = box[slotIndex];

        Assert.That((slot.label, slot.focus), Is.EqualTo((expectedLabel, expectedFocus)));
    }

    [TestCase("example-1.txt", 145)]
    public void Test_Runtime_SumOfFocus(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfFocus, Is.EqualTo(expected));
    }

    [TestCase("rn=1", 30)]
    [TestCase("cm-", 253)]
    [TestCase("qp=3", 97)]
    [TestCase("cm=2", 47)]
    [TestCase("qp-", 14)]
    [TestCase("pc=4", 180)]
    [TestCase("ot=9", 9)]
    [TestCase("ab=5", 197)]
    [TestCase("pc-", 48)]
    [TestCase("pc=6", 214)]
    [TestCase("ot=7", 231)]
    public void Test_Hash_Code(string value, int expected) {
        var sut = new Hash(value);

        Assert.That(sut.code, Is.EqualTo(expected));
    }

    [TestCase("rn=1", "rn")]
    [TestCase("cm-", "cm")]
    [TestCase("qp=3", "qp")]
    [TestCase("cm=2", "cm")]
    [TestCase("qp-", "qp")]
    [TestCase("pc=4", "pc")]
    [TestCase("ot=9", "ot")]
    [TestCase("ab=5", "ab")]
    [TestCase("pc-", "pc")]
    [TestCase("pc=6", "pc")]
    [TestCase("ot=7", "ot")]
    public void Test_Hash_Label(string value, string expected) {
        var sut = new Hash(value);

        Assert.That(sut.label, Is.EqualTo(expected));
    }

    [TestCase("rn=1", 1)]
    [TestCase("qp=3", 3)]
    [TestCase("cm=2", 2)]
    public void Test_Hash_Focus(string value, int expected) {
        var sut = new Hash(value);

        Assert.That(sut.focus, Is.EqualTo(expected));
    }

    [TestCase("rn=1", true)]
    [TestCase("cm-", false)]
    public void Test_Hash_IsEqual(string value, bool expected) {
        var sut = new Hash(value);

        Assert.That(sut.isEqual, Is.EqualTo(expected));
    }
}