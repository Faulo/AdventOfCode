namespace Day07;

class NoSpaceLeftOnDevice {
    const string INPUT_FOLDER = "input";

    readonly string path;

    internal NoSpaceLeftOnDevice(string file) {
        path = Path.Combine(INPUT_FOLDER, file);
    }

    internal int FindTotalSizeOfSmallDirectories() => throw new NotImplementedException();
}
