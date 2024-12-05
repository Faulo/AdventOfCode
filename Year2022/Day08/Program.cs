using Day08;

Console.WriteLine($"{nameof(Runtime.visibleTreeCount)}: {new Runtime("example.txt").visibleTreeCount}");

Console.WriteLine($"{nameof(Runtime.visibleTreeCount)}: {new Runtime("input.txt").visibleTreeCount}");

Console.WriteLine($"{nameof(Runtime.highestScenicScore)}: {new Runtime("example.txt").highestScenicScore}");

Console.WriteLine($"{nameof(Runtime.highestScenicScore)}: {new Runtime("input.txt").highestScenicScore}");
