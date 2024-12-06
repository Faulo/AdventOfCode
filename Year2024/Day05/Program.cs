using Day05;

Console.WriteLine($"{nameof(Runtime.sumOfCorrectMiddle)}: {new Runtime("example-1.txt").sumOfCorrectMiddle}");

Console.WriteLine($"{nameof(Runtime.sumOfCorrectMiddle)}: {new Runtime("input.txt").sumOfCorrectMiddle}");

Console.WriteLine($"{nameof(Runtime.sumOfWrongMiddle)}: {new Runtime("example-1.txt").sumOfWrongMiddle}");

Console.WriteLine($"{nameof(Runtime.sumOfWrongMiddle)}: {new Runtime("input.txt").sumOfWrongMiddle}");
