﻿using Year2024.Day03;

Console.WriteLine($"{nameof(Runtime.multSum)}: {new Runtime("example-1.txt").multSum}");

Console.WriteLine($"{nameof(Runtime.multSum)}: {new Runtime("input.txt").multSum}");

Console.WriteLine($"{nameof(Runtime.multSum)}: {new Runtime("example-1.txt", true).multSum}");

Console.WriteLine($"{nameof(Runtime.multSum)}: {new Runtime("input.txt", true).multSum}");
