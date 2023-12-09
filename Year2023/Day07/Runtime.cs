using Utilities;

namespace Day07;

sealed class Runtime {
    internal class CardComparer : IComparer<char> {
        internal static CardComparer instance = new();

        int GetRank(char card) => card switch {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            '9' => 9,
            '8' => 8,
            '7' => 7,
            '6' => 6,
            '5' => 5,
            '4' => 4,
            '3' => 3,
            '2' => 2,
            _ => throw new NotImplementedException(),
        };

        public int Compare(char x, char y) {
            return GetRank(x).CompareTo(GetRank(y));
        }
    }
    internal record Hand : IComparable<Hand> {
        readonly string cards;
        readonly Dictionary<char, int> counts;

        internal readonly int bid;
        internal readonly int rank;

        int CalculateRank() {
            if (counts.Count == 1) {
                return 7;
            }

            if (counts.Values.Any(value => value == 4)) {
                return 6;
            }

            if (counts.Values.Any(value => value == 3)) {
                return counts.Values.Any(value => value == 2)
                    ? 5
                    : 4;
            }

            return counts.Values.Count(value => value == 2) + 1;
        }

        public Hand(string cards, int bid = 0) {
            this.cards = cards;
            this.bid = bid;

            counts = [];
            foreach (char c in cards.ToCharArray()) {
                if (!counts.ContainsKey(c)) {
                    counts[c] = 0;
                }

                counts[c]++;
            }

            rank = CalculateRank();
        }

        public int CompareTo(Hand? other) {
            if (other is null) {
                throw new NotImplementedException();
            }

            if (cards == other.cards) {
                return 0;
            }

            if (rank != other.rank) {
                return rank.CompareTo(other.rank);
            }

            for (int i = 0; i < 5; i++) {
                int compare = CardComparer.instance.Compare(cards[i], other.cards[i]);
                if (compare != 0) {
                    return compare;
                }
            }

            return 0;
        }
    }

    internal readonly List<Hand> hands = [];

    public long sumOfWinnings {
        get {
            long sum = 0;
            int i = 1;
            foreach (var hand in hands.OrderBy(h => h)) {
                sum += hand.bid * i;
                i++;
            }

            return sum;
        }
    }

    internal Runtime(string file) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] values = line.Split(' ');
            hands.Add(new Hand(values[0], int.Parse(values[1])));
        }
    }
}