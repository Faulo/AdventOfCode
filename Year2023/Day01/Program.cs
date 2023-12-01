using Day01;

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("example-1.txt").calibrationSum}");

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("example-2.txt").calibrationSum}");

Console.WriteLine($"{nameof(Runtime.calibrationSum)}: {new Runtime("input.txt").calibrationSum}");
