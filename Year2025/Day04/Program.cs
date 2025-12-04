using Day04;

Console.WriteLine($"{nameof(Runtime.accessibleRollCount)}: {new Runtime("example.txt").accessibleRollCount}");

Console.WriteLine($"{nameof(Runtime.accessibleRollCount)}: {new Runtime("input.txt").accessibleRollCount}");

Console.WriteLine($"{nameof(Runtime.accessibleRollCountWithRemoving)}: {new Runtime("example.txt").accessibleRollCountWithRemoving}");

Console.WriteLine($"{nameof(Runtime.accessibleRollCountWithRemoving)}: {new Runtime("input.txt").accessibleRollCountWithRemoving}");
