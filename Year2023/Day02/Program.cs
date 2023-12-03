using Day02;

Console.WriteLine($"{nameof(Runtime.CalculatePossible)} example-1.txt: {new Runtime().CalculatePossible("example-1.txt")}");

Console.WriteLine($"{nameof(Runtime.CalculatePossible)} input.txt: {new Runtime().CalculatePossible("input.txt")}");

Console.WriteLine($"{nameof(Runtime.CalculatePower)} example-1.txt: {Runtime.CalculatePower("example-1.txt")}");

Console.WriteLine($"{nameof(Runtime.CalculatePower)} input.txt: {Runtime.CalculatePower("input.txt")}");
