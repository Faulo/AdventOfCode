using Day11;

Console.WriteLine($"{nameof(Runtime.sumOfShortestPaths)} example-1.txt: {new Runtime("example-1.txt").sumOfShortestPaths}");

Console.WriteLine($"{nameof(Runtime.sumOfShortestPaths)} input.txt: {new Runtime("input.txt").sumOfShortestPaths}");

Console.WriteLine($"{nameof(Runtime.sumOfShortestPaths)} input.txt: {new Runtime("input.txt", 1000000).sumOfShortestPaths}");
