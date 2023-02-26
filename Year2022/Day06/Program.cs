using Day06;

Console.WriteLine($"{nameof(TuningTrouble.FindStartInFile)}: {new TuningTrouble("input.txt").FindStartInFile(MessageType.StartOfPacket)}");

Console.WriteLine($"{nameof(TuningTrouble.FindStartInFile)}: {new TuningTrouble("input.txt").FindStartInFile(MessageType.StartOfMessage)}");
