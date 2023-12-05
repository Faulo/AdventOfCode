using Day05;

Console.WriteLine($"{nameof(Runtime.lowestLocation)} example-1.txt: {new Runtime("example-1.txt").lowestLocation}");

Console.WriteLine($"{nameof(Runtime.lowestLocation)} input.txt: {new Runtime("input.txt").lowestLocation}");

Console.WriteLine($"{nameof(Runtime.lowestLocationOfPairs)} example-1.txt: {new Runtime("example-1.txt").lowestLocationOfPairs}");

Console.WriteLine($"{nameof(Runtime.lowestLocationOfPairs)} input.txt: {new Runtime("input.txt").lowestLocationOfPairs}");
