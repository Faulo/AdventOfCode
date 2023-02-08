namespace Day02;

class Program {
    const string INPUT_FOLDER = "input";

    public static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    public static int CalculateTotalScore(string file) {
        return ReadFileToArray(file)
            .Sum(CalculateLineScore);
    }

    public static int CalculateLineScore(string line) {
        return CalculateLinePickScore(line) + CalculateLineWinScore(line);
    }

    const int SCORE_ROCK = 1;
    const int SCORE_PAPER = 2;
    const int SCORE_SCISSORS = 3;
    public static int CalculateLinePickScore(string line) {
        return line.Split(' ') switch {
            [_, "X"] => SCORE_ROCK,
            [_, "Y"] => SCORE_PAPER,
            [_, "Z"] => SCORE_SCISSORS,
            _ => throw new NotImplementedException(),
        };
    }

    const int SCORE_WIN = 6;
    const int SCORE_DRAW = 3;
    const int SCORE_LOSS = 0;
    public static int CalculateLineWinScore(string line) {
        return line switch {
            "A X" => SCORE_DRAW,
            "A Y" => SCORE_WIN,
            "A Z" => SCORE_LOSS,

            "B X" => SCORE_LOSS,
            "B Y" => SCORE_DRAW,
            "B Z" => SCORE_WIN,

            "C X" => SCORE_WIN,
            "C Y" => SCORE_LOSS,
            "C Z" => SCORE_DRAW,
            _ => throw new NotImplementedException(),
        };
    }
}