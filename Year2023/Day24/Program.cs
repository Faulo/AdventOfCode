using Day24;

Console.WriteLine($"{nameof(Runtime.GetNumberOfCollisions)} example-1.txt: {new Runtime("example-1.txt").GetNumberOfCollisions(7, 27)}");

Console.WriteLine($"{nameof(Runtime.GetNumberOfCollisions)} input.txt: {new Runtime("input.txt").GetNumberOfCollisions(200000000000000, 400000000000000)}");
