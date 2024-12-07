using Day07;

Console.WriteLine($"{nameof(Runtime.pathCount)}: {new Runtime("example-1.txt").pathCount}");

Console.WriteLine($"{nameof(Runtime.pathCount)}: {new Runtime("input.txt").pathCount}");

Console.WriteLine($"{nameof(Runtime.obstructionCount)}: {new Runtime("example-1.txt").obstructionCount}");

Console.WriteLine($"{nameof(Runtime.obstructionCount)}: {new Runtime("input.txt").obstructionCount}");

