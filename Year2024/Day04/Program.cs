using Day04;

Console.WriteLine($"{nameof(Runtime.straightOccurences)}: {new Runtime("example-1.txt").straightOccurences}");

Console.WriteLine($"{nameof(Runtime.straightOccurences)}: {new Runtime("input.txt").straightOccurences}");

Console.WriteLine($"{nameof(Runtime.straightOccurences)}: {new Runtime("example-1.txt").crossOccurences}");

Console.WriteLine($"{nameof(Runtime.straightOccurences)}: {new Runtime("input.txt").crossOccurences}");
