using Day07;

Console.WriteLine($"{nameof(Runtime.splitCount)}: {new Runtime("example.txt").splitCount}");

Console.WriteLine($"{nameof(Runtime.splitCount)}: {new Runtime("input.txt").splitCount}");

Console.WriteLine($"{nameof(Runtime.timelineCount)}: {new Runtime("example.txt").timelineCount}");

Console.WriteLine($"{nameof(Runtime.timelineCount)}: {new Runtime("input.txt").timelineCount}");
