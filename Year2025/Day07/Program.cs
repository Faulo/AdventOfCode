using Day07;

Console.WriteLine($"{nameof(Runtime.freshIngredients)}: {new Runtime("example.txt").freshIngredients}");

Console.WriteLine($"{nameof(Runtime.freshIngredients)}: {new Runtime("input.txt").freshIngredients}");

Console.WriteLine($"{nameof(Runtime.allFreshIngredients)}: {new Runtime("example.txt").allFreshIngredients}");

Console.WriteLine($"{nameof(Runtime.allFreshIngredients)}: {new Runtime("input.txt").allFreshIngredients}");
