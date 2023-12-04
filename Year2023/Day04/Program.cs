using Day04;

Console.WriteLine($"{nameof(Runtime.sumOfWins)} example-1.txt: {new Runtime("example-1.txt").sumOfWins}");

Console.WriteLine($"{nameof(Runtime.sumOfWins)} input.txt: {new Runtime("input.txt").sumOfWins}");

Console.WriteLine($"{nameof(Runtime.sumOfCards)} example-1.txt: {new Runtime("example-1.txt").sumOfCards}");

Console.WriteLine($"{nameof(Runtime.sumOfCards)} input.txt: {new Runtime("input.txt").sumOfCards}");
