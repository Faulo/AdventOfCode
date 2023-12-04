using Day02;

Console.WriteLine($"{nameof(Runtime.sumOfPossible)} example-1.txt: {new Runtime("example-1.txt").sumOfPossible}");

Console.WriteLine($"{nameof(Runtime.sumOfPossible)} input.txt: {new Runtime("input.txt").sumOfPossible}");

Console.WriteLine($"{nameof(Runtime.sumOfProducts)} example-1.txt: {new Runtime("example-1.txt").sumOfProducts}");

Console.WriteLine($"{nameof(Runtime.sumOfProducts)} input.txt: {new Runtime("input.txt").sumOfProducts}");
