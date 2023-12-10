namespace Day10;

sealed class Runtime {
    readonly string file;

    internal int maximumDistance => 0;

    internal Runtime(string file) {
        this.file = file;
    }
}