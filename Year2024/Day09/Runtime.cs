using Utilities;

namespace Day09;

sealed partial class Runtime {
    readonly string manifest;

    internal Runtime(string file) {
        manifest = new FileInput(file).ReadLines().First();
    }

    internal long defragChecksum {
        get {
            return 0;
        }
    }

    internal long complexAntinodeCount {
        get {
            return 0;
        }
    }
}