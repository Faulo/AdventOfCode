using Day07;

Console.WriteLine($"{nameof(Runtime.totalSizeOfSmallDirectories)}: {new Runtime("example.txt").totalSizeOfSmallDirectories}");

Console.WriteLine($"{nameof(Runtime.totalSizeOfSmallDirectories)}: {new Runtime("input.txt").totalSizeOfSmallDirectories}");

Console.WriteLine($"{nameof(Runtime.totalSizeOfDeletableDirectory)}: {new Runtime("example.txt").totalSizeOfDeletableDirectory}");

Console.WriteLine($"{nameof(Runtime.totalSizeOfDeletableDirectory)}: {new Runtime("input.txt").totalSizeOfDeletableDirectory}");
