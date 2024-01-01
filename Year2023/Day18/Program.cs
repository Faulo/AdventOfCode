using Day18;

Console.WriteLine($"{nameof(Runtime.totalDigArea)} example-1.txt: {new Runtime("example-1.txt").totalDigArea}");

Console.WriteLine($"{nameof(Runtime.totalDigArea)} input.txt: {new Runtime("input.txt").totalDigArea}");

Console.WriteLine($"{nameof(Runtime.totalDigArea)} example-1.txt: {new Runtime("example-1.txt", true).totalDigArea}");

Console.WriteLine($"{nameof(Runtime.totalDigArea)} input.txt: {new Runtime("input.txt", true).totalDigArea}");
