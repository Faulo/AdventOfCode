using NUnit.Framework;

namespace Day19;

[TestFixture(TestOf = typeof(Runtime))]
public class Tests {
    [TestCase("example-1.txt", 19114)]
    public void Test_Runtime_SumOfAcceptedParts(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.sumOfAcceptedParts, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 167409079868000)]
    public void Test_Runtime_DistinctCombinationsOfAcceptedParts(string file, long expected) {
        var sut = new Runtime(file);

        Assert.That(sut.distinctCombinationsOfAcceptedParts, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 11)]
    public void Test_Runtime_WorfklowCount(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.workflows.Count, Is.EqualTo(expected));
    }
    [TestCase("example-1.txt", 5)]
    public void Test_Runtime_PartCount(string file, int expected) {
        var sut = new Runtime(file);

        Assert.That(sut.parts.Count, Is.EqualTo(expected));
    }

    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", "ex")]
    public void Test_Workflow_Parse_Name(string workflow, string expected) {
        var sut = Workflow.Parse(workflow);

        Assert.That(sut.name, Is.EqualTo(expected));
    }

    [TestCase("{x=787,m=2655,a=1222,s=2876}", 787, 2655, 1222, 2876)]
    [TestCase("{x=1679,m=44,a=2067,s=496}", 1679, 44, 2067, 496)]
    public void Test_Part_ParseValues(string part, int x, int m, int a, int s) {
        var sut = Part.Parse(part);

        Assert.That(sut.x, Is.EqualTo(x));
        Assert.That(sut.m, Is.EqualTo(m));
        Assert.That(sut.a, Is.EqualTo(a));
        Assert.That(sut.s, Is.EqualTo(s));
    }

    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", 0, 30, 0, "A")]
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", 0, 30, 31, "R")]
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", 0, 19, 31, "two")]
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", 11, 19, 31, "one")]
    public void Test_Workflow_Process(string workflow, int x, int m, int a, string expected) {
        var sut = Workflow.Parse(workflow);

        var part = new Part(x, m, a, 0);

        Assert.That(sut.Process(part), Is.EqualTo(expected));
    }
}