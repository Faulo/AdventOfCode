using Day14;

Console.WriteLine($"{nameof(Runtime.northLoad)} example-1.txt: {new Runtime("example-1.txt").TiltNorth().northLoad}");

Console.WriteLine($"{nameof(Runtime.northLoad)} input.txt: {new Runtime("input.txt").TiltNorth().northLoad}");

Console.WriteLine($"{nameof(Runtime.CycleTilt)} input.txt: {new Runtime("input.txt").CycleTilt(1000000000).northLoad}");

