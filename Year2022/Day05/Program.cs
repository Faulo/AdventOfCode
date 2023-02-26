using Day05;

Console.WriteLine($"{nameof(SupplyStacks.ExecuteAndPrint)}: {new SupplyStacks("input.txt").ExecuteAndPrint()}");

Console.WriteLine($"{nameof(SupplyStacks.ExecuteAndPrint)}: {new SupplyStacks("input.txt", Model.CrateMover9001).ExecuteAndPrint()}");
