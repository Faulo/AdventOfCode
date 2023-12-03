using Day03;

Console.WriteLine($"{nameof(Runtime.sumOfAdjacentParts)} example-1.txt: {new Runtime("example-1.txt").sumOfAdjacentParts}");

Console.WriteLine($"{nameof(Runtime.sumOfAdjacentParts)} input.txt: {new Runtime("input.txt").sumOfAdjacentParts}");

Console.WriteLine($"{nameof(Runtime.sumOfAdjacentGears)} example-1.txt: {new Runtime("example-1.txt").sumOfAdjacentGears}");

Console.WriteLine($"{nameof(Runtime.sumOfAdjacentGears)} input.txt: {new Runtime("input.txt").sumOfAdjacentGears}");
