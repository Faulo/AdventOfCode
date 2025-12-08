using Day08;

Console.WriteLine($"{nameof(Runtime.circuitAggregate)}: {new Runtime("example.txt", 10).circuitAggregate}");

Console.WriteLine($"{nameof(Runtime.circuitAggregate)}: {new Runtime("input.txt", 1000).circuitAggregate}");

Console.WriteLine($"{nameof(Runtime.allFreshIngredients)}: {new Runtime("example.txt", 10).allFreshIngredients}");

Console.WriteLine($"{nameof(Runtime.allFreshIngredients)}: {new Runtime("input.txt", 1000).allFreshIngredients}");
