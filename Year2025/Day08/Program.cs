using Day08;

Console.WriteLine($"{nameof(Runtime.circuitAggregate)}: {new Runtime("example.txt", 10).circuitAggregate}");

Console.WriteLine($"{nameof(Runtime.circuitAggregate)}: {new Runtime("input.txt", 1000).circuitAggregate}");

Console.WriteLine($"{nameof(Runtime.lastPairXProduct)}: {new Runtime("example.txt").lastPairXProduct}");

Console.WriteLine($"{nameof(Runtime.lastPairXProduct)}: {new Runtime("input.txt").lastPairXProduct}");
