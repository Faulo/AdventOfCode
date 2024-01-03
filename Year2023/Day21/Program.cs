using Day21;

Console.WriteLine($"{nameof(Runtime.GetNumberOfDestinations)} example-1.txt: {new Runtime("example-1.txt").GetNumberOfDestinations(6)}");

Console.WriteLine($"{nameof(Runtime.GetNumberOfDestinations)} input.txt: {new Runtime("input.txt").GetNumberOfDestinations(64)}");
