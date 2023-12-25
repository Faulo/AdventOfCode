using Day12;

Console.WriteLine($"{nameof(Runtime.sumOfArrangements)} example-1.txt: {new Runtime("example-1.txt").sumOfArrangements}");

Console.WriteLine($"{nameof(Runtime.sumOfArrangements)} input.txt: {new Runtime("input.txt").sumOfArrangements}");

Console.WriteLine($"{nameof(Runtime.sumOfArrangements)} example-1.txt: {new Runtime("example-1.txt", 5).sumOfArrangements}");

Console.WriteLine($"{nameof(Runtime.sumOfArrangements)} input.txt: {new Runtime("input.txt", 5).sumOfArrangements}");

