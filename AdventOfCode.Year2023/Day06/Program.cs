using Day06;

Console.WriteLine($"{nameof(TuningTrouble.FindStartInFile)}: {TuningTrouble.FindStartInFile("input.txt", MessageType.StartOfPacket)}");

Console.WriteLine($"{nameof(TuningTrouble.FindStartInFile)}: {TuningTrouble.FindStartInFile("input.txt", MessageType.StartOfMessage)}");
