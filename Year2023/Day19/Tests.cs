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
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", "{x=11,m=1,a=1,s=1}", "{x=4000,m=4000,a=4000,s=4000}", "one")]
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", "{x=1,m=1,a=1,s=1}", "{x=10,m=20,a=4000,s=4000}", "two")]
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", "{x=1,m=21,a=31,s=1}", "{x=10,m=4000,a=4000,s=4000}", "R")]
    [TestCase("ex{x>10:one,m<20:two,a>30:R,A}", "{x=1,m=21,a=1,s=1}", "{x=10,m=4000,a=30,s=4000}", "A")]
    public void Test_Workflow_GetRanges(string workflow, string expectedMin, string expectedMax, string expectedStep) {
        var range = (new Part(1, 1, 1, 1), new Part(4000, 4000, 4000, 4000));

        var sut = Workflow.Parse(workflow);

        var expected = (
            (Part.Parse(expectedMin), Part.Parse(expectedMax)),
            expectedStep
        );

        Assert.That(sut.GetRanges(range), Contains.Item(expected));
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