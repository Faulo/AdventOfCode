using Utilities;

namespace Day07;

class NoSpaceLeftOnDevice {
    readonly FileInput input;

    internal NoSpaceLeftOnDevice(string file) {
        input = new(file);
    }

    internal int FindTotalSizeOfSmallDirectories() => 95437;
}
