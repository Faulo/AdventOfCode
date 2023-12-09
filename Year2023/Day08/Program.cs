using Day08;

Console.WriteLine($"{nameof(Runtime.numberOfSteps)} example-1.txt: {new Runtime("example-1.txt").numberOfSteps}");

Console.WriteLine($"{nameof(Runtime.numberOfSteps)} input.txt: {new Runtime("input.txt").numberOfSteps}");

Console.WriteLine($"{nameof(Runtime.numberOfGhostSteps)} example-1.txt: {new Runtime("example-1.txt").numberOfGhostSteps}");

Console.WriteLine($"{nameof(Runtime.numberOfGhostSteps)} input.txt: {new Runtime("input.txt").numberOfGhostSteps}");
