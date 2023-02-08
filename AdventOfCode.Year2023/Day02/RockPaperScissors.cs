namespace Day02;

class RockPaperScissors {
    const string INPUT_FOLDER = "input";

    public static IEnumerable<string> ReadFileToArray(string file) {
        return File.ReadLines(Path.Combine(INPUT_FOLDER, file));
    }

    public static int CalculateTotalScore(string file, MatchFormat format) {
        return ReadFileToArray(file)
            .Sum(line => CalculateLineScore(line, format));
    }

    public static int CalculateLineScore(string line, MatchFormat format) {
        return CalculateLinePickScore(line, format) + CalculateLineWinScore(line, format);
    }

    const int SCORE_ROCK = 1;
    const int SCORE_PAPER = 2;
    const int SCORE_SCISSORS = 3;
    public static int CalculateLinePickScore(string line, MatchFormat format) {
        return format switch {
            MatchFormat.PickAndPick => line.Split(' ') switch {
                [_, "X"] => SCORE_ROCK,
                [_, "Y"] => SCORE_PAPER,
                [_, "Z"] => SCORE_SCISSORS,
                _ => throw new NotImplementedException(),
            },
            MatchFormat.PickAndResult => line switch {
                "A X" => SCORE_SCISSORS,
                "A Y" => SCORE_ROCK,
                "A Z" => SCORE_PAPER,

                "B X" => SCORE_ROCK,
                "B Y" => SCORE_PAPER,
                "B Z" => SCORE_SCISSORS,

                "C X" => SCORE_PAPER,
                "C Y" => SCORE_SCISSORS,
                "C Z" => SCORE_ROCK,
                _ => throw new NotImplementedException(),
            },
            _ => throw new NotImplementedException()
        };
    }

    const int SCORE_WIN = 6;
    const int SCORE_DRAW = 3;
    const int SCORE_LOSS = 0;
    public static int CalculateLineWinScore(string line, MatchFormat format) {
        return format switch {
            MatchFormat.PickAndPick => line switch {
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
            },
            MatchFormat.PickAndResult => line.Split(' ') switch {
                [_, "X"] => SCORE_LOSS,
                [_, "Y"] => SCORE_DRAW,
                [_, "Z"] => SCORE_WIN,
                _ => throw new NotImplementedException(),
            },
            _ => throw new NotImplementedException(),
        };
    }
}