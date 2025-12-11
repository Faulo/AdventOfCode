using Day11;

Console.WriteLine($"{nameof(Runtime.youCount)}: {new Runtime("example.txt").youCount}");

Console.WriteLine($"{nameof(Runtime.youCount)}: {new Runtime("input.txt").youCount}");

Console.WriteLine($"{nameof(Runtime.serverCount)}: {new Runtime("example-2.txt").serverCount}");

Console.WriteLine($"{nameof(Runtime.serverCount)}: {new Runtime("input.txt").serverCount}");
