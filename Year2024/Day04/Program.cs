using Day04;

Console.WriteLine($"{nameof(Runtime.occurences)}: {new Runtime("example-1.txt").occurences}");

Console.WriteLine($"{nameof(Runtime.occurences)}: {new Runtime("input.txt").occurences}");

Console.WriteLine($"{nameof(Runtime.occurences)}: {new Runtime("example-1.txt", true).occurences}");

Console.WriteLine($"{nameof(Runtime.occurences)}: {new Runtime("input.txt", true).occurences}");
