using Day23;

Console.WriteLine($"{nameof(Runtime.maximumNumberOfSteps)} example-1.txt: {new Runtime("example-1.txt").maximumNumberOfSteps}");

Console.WriteLine($"{nameof(Runtime.maximumNumberOfSteps)} input.txt: {new Runtime("input.txt").maximumNumberOfSteps}");

Console.WriteLine($"{nameof(Runtime.maximumNumberOfSteps)} example-1.txt: {new Runtime("example-1.txt", true).maximumNumberOfSteps}");

Console.WriteLine($"{nameof(Runtime.maximumNumberOfSteps)} input.txt: {new Runtime("input.txt", true).maximumNumberOfSteps}");
