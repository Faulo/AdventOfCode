using Day13;

Console.WriteLine($"{nameof(Runtime.sumOfReflections)} example-1.txt: {new Runtime("example-1.txt").sumOfReflections}");

Console.WriteLine($"{nameof(Runtime.sumOfReflections)} input.txt: {new Runtime("input.txt").sumOfReflections}");

Console.WriteLine($"{nameof(Runtime.sumOfReflectionsWithSmudgesFixed)} example-1.txt: {new Runtime("example-1.txt").sumOfReflectionsWithSmudgesFixed}");

Console.WriteLine($"{nameof(Runtime.sumOfReflectionsWithSmudgesFixed)} input.txt: {new Runtime("input.txt").sumOfReflectionsWithSmudgesFixed}");
