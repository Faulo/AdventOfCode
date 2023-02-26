using Day02;

foreach (var format in new[] { MatchFormat.PickAndPick, MatchFormat.PickAndResult }) {
    Console.WriteLine($"Total Score ({format}): {RockPaperScissors.CalculateTotalScore("input.txt", format)}");
}
