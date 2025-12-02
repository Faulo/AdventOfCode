using Day02;

Console.WriteLine($"{nameof(Runtime.sumOfInvalidIds)}: {new Runtime("example.txt").sumOfInvalidIds}");

Console.WriteLine($"{nameof(Runtime.sumOfInvalidIds)}: {new Runtime("input.txt").sumOfInvalidIds}");

Console.WriteLine($"{nameof(Runtime.sumOfAllInvalidIds)}: {new Runtime("example.txt").sumOfAllInvalidIds}");

Console.WriteLine($"{nameof(Runtime.sumOfAllInvalidIds)}: {new Runtime("input.txt").sumOfAllInvalidIds}");

