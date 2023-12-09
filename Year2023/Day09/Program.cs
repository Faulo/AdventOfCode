using Day09;

Console.WriteLine($"{nameof(Runtime.sumOfExtrapolations)} example-1.txt: {new Runtime("example-1.txt").sumOfExtrapolations}");

Console.WriteLine($"{nameof(Runtime.sumOfExtrapolations)} input.txt: {new Runtime("input.txt").sumOfExtrapolations}");

Console.WriteLine($"{nameof(Runtime.sumOfBackwardsExtrapolations)} example-1.txt: {new Runtime("example-1.txt").sumOfBackwardsExtrapolations}");

Console.WriteLine($"{nameof(Runtime.sumOfBackwardsExtrapolations)} input.txt: {new Runtime("input.txt").sumOfBackwardsExtrapolations}");
