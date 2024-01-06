namespace Day24;

sealed class Runtime {
    internal int numberOfCollisions {
        get {
            int count = 0;

            return count;
        }
    }

    long min;
    long max;

    internal Runtime(string file, long min, long max) {
        this.min = min;
        this.max = max;
    }
}

static class Extensions {
}