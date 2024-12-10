using Day09;

Console.WriteLine($"{nameof(Runtime.defragChecksum)}: {new Runtime("example-1.txt").defragChecksum}");

Console.WriteLine($"{nameof(Runtime.defragChecksum)}: {new Runtime("input.txt").defragChecksum}");

Console.WriteLine($"{nameof(Runtime.complexAntinodeCount)}: {new Runtime("example-1.txt").complexAntinodeCount}");

Console.WriteLine($"{nameof(Runtime.complexAntinodeCount)}: {new Runtime("input.txt").complexAntinodeCount}");
