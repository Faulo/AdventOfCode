using Day03;

Console.WriteLine($"{nameof(Runtime.totalJoltage)}: {new Runtime("example.txt").totalJoltage}");

Console.WriteLine($"{nameof(Runtime.totalJoltage)}: {new Runtime("input.txt").totalJoltage}");

Console.WriteLine($"{nameof(Runtime.totalJoltage)}: {new Runtime("example.txt", 12).totalJoltage}");

Console.WriteLine($"{nameof(Runtime.totalJoltage)}: {new Runtime("input.txt", 12).totalJoltage}");
