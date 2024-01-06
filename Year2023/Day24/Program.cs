using Day24;

Console.WriteLine($"{nameof(Runtime.numberOfCollisions)} example-1.txt: {new Runtime("example-1.txt", 7, 27).numberOfCollisions}");

Console.WriteLine($"{nameof(Runtime.numberOfCollisions)} input.txt: {new Runtime("input.txt", 200000000000000, 400000000000000).numberOfCollisions}");
