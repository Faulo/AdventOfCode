namespace Day04;

static class CampCleanup {
    const string INPUT_FOLDER = "input";

    internal static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    internal static ((int, int), (int, int)) DivideIntoSections(string line) {
        string[] sections = line.Split(',');
        return (DivideSection(sections[0]), DivideSection(sections[1]));
    }

    static (int, int) DivideSection(string section) {
        string[] ids = section.Split('-');
        return (int.Parse(ids[0]), int.Parse(ids[1]));
    }

    internal static bool DoSectionsOverlapCompletely((int start, int stop) section1, (int start, int stop) section2) {
        return (section1.Contains(section2.start) && section1.Contains(section2.stop))
            || (section2.Contains(section1.start) && section2.Contains(section1.stop));
    }

    internal static bool DoSectionsOverlap((int start, int stop) section1, (int start, int stop) section2) {
        return section1.Contains(section2.start) || section1.Contains(section2.stop)
            || section2.Contains(section1.start) || section2.Contains(section1.stop);
    }

    static bool Contains(this (int start, int stop) section, int id) {
        return section.start <= id && id <= section.stop;
    }

    internal static int SumOfCompletelyOverlappingSections(string file) {
        return ReadFileToArray(file)
            .Select(DivideIntoSections)
            .Count(sections => DoSectionsOverlapCompletely(sections.Item1, sections.Item2));
    }

    internal static int SumOfOverlappingSections(string file) {
        return ReadFileToArray(file)
            .Select(DivideIntoSections)
            .Count(sections => DoSectionsOverlap(sections.Item1, sections.Item2));
    }
}
