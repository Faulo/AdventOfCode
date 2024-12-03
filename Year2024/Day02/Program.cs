using Day01;

Console.WriteLine($"{nameof(Runtime.safeReports)}: {new Runtime("example-1.txt").safeReports}");

Console.WriteLine($"{nameof(Runtime.safeReports)}: {new Runtime("input.txt").safeReports}");

Console.WriteLine($"{nameof(Runtime.safeReports)}: {new Runtime("example-1.txt", true).safeReports}");

Console.WriteLine($"{nameof(Runtime.safeReports)}: {new Runtime("input.txt", true).safeReports}");

