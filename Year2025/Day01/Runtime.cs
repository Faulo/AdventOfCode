using Utilities;

namespace Day01;

sealed class Runtime {
    readonly List<(int direction, int amount)> instructions = [];

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            int direction = line[0] switch {
                'L' => -1,
                'R' => 1,
                _ => 0,
            };

            if (direction != 0) {
                int amount = int.Parse(line[1..]);
                instructions.Add((direction, amount));
            }
        }
    }

    IEnumerable<int> dial {
        get {
            int current = 50;
            foreach ((int direction, int amount) in instructions) {
                current += amount * direction;
                while (current < 0) {
                    current += 100;
                }

                while (current >= 100) {
                    current -= 100;
                }

                yield return current;
            }
        }
    }

    internal int dialZeroCount => dial
        .Count(d => d == 0);

    internal int passZeroCount {
        get {
            int count = 0;
            int current = 50;
            foreach ((int direction, int amount) in instructions) {
                for (int i = 0; i < amount; i++) {
                    current += direction;
                    if (current % 100 == 0) {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}