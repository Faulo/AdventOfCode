namespace Day04;

class CampCleanup {
    const string INPUT_FOLDER = "input";

    public static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    public static ((int, int), (int, int)) DivideIntoSections(string line) {
        string[] sections = line.Split(',');
        return (DivideSection(sections[0]), DivideSection(sections[1]));
    }

    static (int, int) DivideSection(string section) {
        string[] ids = section.Split('-');
        return (int.Parse(ids[0]), int.Parse(ids[1]));
    }

    public static bool DoSectionsOverlapCompletely((int start, int stop) section1, (int start, int stop) section2) {
        return (section1.start >= section2.start && section1.stop <= section2.stop)
            || (section2.start >= section1.start && section2.stop <= section1.stop);
    }

    public static int SumOfOverlappingSections(string file) {
        return ReadFileToArray(file)
            .Select(DivideIntoSections)
            .Count(sections => DoSectionsOverlapCompletely(sections.Item1, sections.Item2));
    }
}
