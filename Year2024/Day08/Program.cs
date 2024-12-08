using Day08;

Console.WriteLine($"{nameof(Runtime.simpleAntinodeCount)}: {new Runtime("example-1.txt").simpleAntinodeCount}");

Console.WriteLine($"{nameof(Runtime.simpleAntinodeCount)}: {new Runtime("input.txt").simpleAntinodeCount}");

Console.WriteLine($"{nameof(Runtime.complexAntinodeCount)}: {new Runtime("example-1.txt").complexAntinodeCount}");

Console.WriteLine($"{nameof(Runtime.complexAntinodeCount)}: {new Runtime("input.txt").complexAntinodeCount}");
