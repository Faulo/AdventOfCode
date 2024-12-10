using Day09;

Console.WriteLine($"{nameof(Runtime.defragChecksum)}: {new Runtime("example-1.txt").defragChecksum}");

Console.WriteLine($"{nameof(Runtime.defragChecksum)}: {new Runtime("input.txt").defragChecksum}");

Console.WriteLine($"{nameof(Runtime.smartDefragChecksum)}: {new Runtime("example-1.txt").smartDefragChecksum}");

Console.WriteLine($"{nameof(Runtime.smartDefragChecksum)}: {new Runtime("input.txt").smartDefragChecksum}");
