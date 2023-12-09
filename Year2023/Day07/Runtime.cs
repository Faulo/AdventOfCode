using Utilities;

namespace Day07;

public sealed class Runtime {
    public enum Rank {
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1,
    }

    internal record CardComparer(bool useJoker) : IComparer<char> {
        int GetRank(char card) => card switch {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' when !useJoker => 11,
            'T' => 10,
            '9' => 9,
            '8' => 8,
            '7' => 7,
            '6' => 6,
            '5' => 5,
            '4' => 4,
            '3' => 3,
            '2' => 2,
            'J' when useJoker => 1,
            _ => throw new NotImplementedException(),
        };

        public int Compare(char x, char y) {
            return GetRank(x).CompareTo(GetRank(y));
        }
    }
    internal record Hand : IComparable<Hand> {
        readonly string cards;
        readonly Dictionary<char, int> counts = [];
        readonly bool useJoker;
        readonly int jokerCount = 0;

        internal readonly int bid;
        internal readonly Rank rank;

        bool HasCountOf(int count, bool useJoker, out char card, params char[] exclude) {
            if (useJoker) {
                if (jokerCount == 5) {
                    card = 'J';
                    return true;
                }

                count -= jokerCount;
            }

            card = counts
                .Where(card => !exclude.Contains(card.Key))
                .FirstOrDefault(card => card.Value == count).Key;

            return card != default;
        }

        Rank CalculateRank() {
            if (HasCountOf(5, useJoker, out _)) {
                return Rank.FiveOfAKind;
            }

            if (HasCountOf(4, useJoker, out _)) {
                return Rank.FourOfAKind;
            }

            if (HasCountOf(3, useJoker, out char three)) {
                return HasCountOf(2, false, out _, three)
                    ? Rank.FullHouse
                    : Rank.ThreeOfAKind;
            }

            return counts.Values.Count(value => value == 2) + (useJoker ? jokerCount : 0) switch {
                2 => Rank.TwoPair,
                1 => Rank.OnePair,
                _ => Rank.HighCard,
            };
        }

        public Hand(string cards, int bid = 0, bool useJoker = false) {
            this.cards = cards;
            this.bid = bid;
            this.useJoker = useJoker;

            counts = [];
            foreach (char c in cards.ToCharArray()) {
                if (useJoker && c == 'J') {
                    jokerCount++;
                    continue;
                }

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

            var comparer = new CardComparer(useJoker);
            for (int i = 0; i < 5; i++) {
                int compare = comparer.Compare(cards[i], other.cards[i]);
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

    internal Runtime(string file, bool useJoker = false) {
        foreach (string line in new FileInput(file).ReadLines()) {
            string[] values = line.Split(' ');
            hands.Add(new Hand(values[0], int.Parse(values[1]), useJoker));
        }
    }
}