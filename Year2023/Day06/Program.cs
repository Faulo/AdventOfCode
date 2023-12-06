using Day06;

Console.WriteLine($"{nameof(Runtime.productOfWins)} example-1.txt: {new Runtime("example-1.txt").productOfWins}");

Console.WriteLine($"{nameof(Runtime.productOfWins)} input.txt: {new Runtime("input.txt").productOfWins}");

Console.WriteLine($"{nameof(Runtime.productOfWins)} example-1.txt: {new Runtime("example-1.txt", true).productOfWins}");

Console.WriteLine($"{nameof(Runtime.productOfWins)} input.txt: {new Runtime("input.txt", true).productOfWins}");
