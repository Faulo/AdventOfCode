namespace Day07;

class NoSpaceLeftOnDevice {
    const string INPUT_FOLDER = "input";

    readonly string path;

    public NoSpaceLeftOnDevice(string file) {
        path = Path.Combine(INPUT_FOLDER, file);
    }

    public int FindTotalSizeOfSmallDirectories() => throw new NotImplementedException();
}
