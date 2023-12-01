using Day01;

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("example-1.txt").calibrationSum}");

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("input.txt").calibrationSum}");

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("example-2.txt", true).calibrationSum}");

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("input.txt", true).calibrationSum}");
