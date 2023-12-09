using Utilities;

namespace Day09;

sealed class Runtime {
    internal record Reading {
        readonly List<List<long>> allReadings = [];

        internal Reading(string reading) {
            var readings = reading
                .Split(' ')
                .Select(long.Parse)
                .ToList();
            do {
                allReadings.Add(readings);
                readings = GetSubReadings(readings);
            } while (readings.Any(v => v != 0));
        }

        List<long> GetSubReadings(List<long> readings) {
            var subReadings = new List<long>();
            for (int i = 1; i < readings.Count; i++) {
                subReadings.Add(readings[i] - readings[i - 1]);
            }

            return subReadings;
        }

        internal long extrapolation {
            get {
                long value = 0;
                for (int i = 0; i < allReadings.Count; i++) {
                    value += allReadings[i][^1];
                }

                return value;
            }
        }

        internal long backwardsExtrapolation {
            get {
                long value = 0;
                for (int i = allReadings.Count - 1; i >= 0; i--) {
                    value = allReadings[i][0] - value;
                }

                return value;
            }
        }
    }

    readonly string file;

    IEnumerable<Reading> readings => new FileInput(file)
        .ReadLines()
        .Select(value => new Reading(value));

    internal long sumOfExtrapolations => readings
        .Sum(r => r.extrapolation);

    internal long sumOfBackwardsExtrapolations => readings
        .Sum(r => r.backwardsExtrapolation);

    internal Runtime(string file) {
        this.file = file;
    }
}