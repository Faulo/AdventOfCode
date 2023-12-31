using Utilities;

namespace Day18;

sealed class Runtime {
    internal int totalDigArea {
        get {
            return 0;
        }
    }

    internal Runtime(string file) {
        new FileInput(file);
    }
}

static class Extensions {
}