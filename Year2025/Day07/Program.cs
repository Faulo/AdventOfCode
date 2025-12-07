using Day07;

Console.WriteLine($"{nameof(Runtime.splitCount)}: {new Runtime("example.txt").splitCount}");

Console.WriteLine($"{nameof(Runtime.splitCount)}: {new Runtime("input.txt").splitCount}");

Console.WriteLine($"{nameof(Runtime.allFreshIngredients)}: {new Runtime("example.txt").allFreshIngredients}");

Console.WriteLine($"{nameof(Runtime.allFreshIngredients)}: {new Runtime("input.txt").allFreshIngredients}");
