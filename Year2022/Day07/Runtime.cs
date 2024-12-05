using Utilities;

namespace Day07;

class Runtime {
    internal abstract class Node {
        internal readonly string path;
        internal readonly string fullPath;
        internal readonly DirectoryNode? parent;

        public Node(string path, DirectoryNode? parent) {
            this.path = path;
            this.parent = parent;

            fullPath = parent is { fullPath: string parentPath }
                ? parentPath + "/" + path
                : path;
        }

        internal abstract int size { get; }
    }

    internal class DirectoryNode : Node {
        internal override int size => children.Sum(child => child.size);

        internal readonly HashSet<Node> children = [];

        public DirectoryNode(string path, DirectoryNode? parent = null) : base(path, parent) {
        }

        internal T AddChild<T>(T node) where T : Node {
            children.Add(node);
            return node;
        }
    }

    internal class FileNode : Node {
        internal override int size { get; }

        public FileNode(string path, DirectoryNode parent, int size) : base(path, parent) {
            this.size = size;
        }
    }

    internal readonly DirectoryNode root = new("");

    DirectoryNode current;

    readonly Dictionary<string, DirectoryNode> directories = [];

    void ChangeDirectory(string path) {
        string fullPath = current.fullPath + "/" + path;

        current = path switch {
            "/" => root,
            ".." => current.parent ?? root,
            _ => directories.TryGetValue(fullPath, out var node)
                ? node
                : directories[fullPath] = current.AddChild(new DirectoryNode(path, current))
        };
    }

    internal Runtime(string logFile) {
        directories[""] = root;
        current = root;

        var input = new FileInput(logFile);
        foreach (string line in input.ReadLines()) {
            switch (line.Split(' ')) {
                case ["$", "cd", string path]:
                    ChangeDirectory(path);
                    break;
                case ["$", "ls"]:
                    break;
                case ["dir", string dir]:
                    break;
                case [string size, string file]:
                    if (int.TryParse(size, out int fileSize)) {
                        current.AddChild(new FileNode(file, current, fileSize));
                    } else {
                        throw new Exception($"Failed to parse size '{size}' of file '{file}'");
                    }

                    break;
                default:
                    throw new Exception($"Failed to parse line '{line}'");
            }
        }
    }

    const int MAX_SPACE = 70000000;
    const int REQUIRED_SPACE = 30000000;

    internal int totalSizeOfSmallDirectories => directories
        .Values
        .Select(d => d.size)
        .Where(s => s <= 100000)
        .Sum();

    internal int totalSizeOfDeletableDirectory {
        get {
            int currentSpace = MAX_SPACE - root.size;
            int neededSpace = REQUIRED_SPACE - currentSpace;

            return directories
                .Values
                .Select(d => d.size)
                .OrderBy(s => s)
                .First(s => s >= neededSpace);
        }
    }
}
