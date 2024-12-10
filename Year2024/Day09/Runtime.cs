using Utilities;

namespace Day09;

sealed partial class Runtime(string file) {
    internal readonly struct HDD {
        internal readonly List<Block> blocks = [];

        internal HDD(string manifest) {
            int id = 0;
            bool isFile = true;
            for (int i = 0; i < manifest.Length; i++) {
                blocks.Add(new(isFile ? id++ : -1, int.Parse(manifest[i..(i + 1)])));

                isFile = !isFile;
            }
        }

        internal void Defrag() {
            for (int i = 0; i < blocks.Count; i++) {
                var space = blocks[i];
                if (space.isFree) {
                    var file = blocks[^1];
                    int delta = space.size - file.size;
                    switch (delta) {
                        case 0:
                            space.id = file.id;
                            file.id = -1;
                            break;
                        case < 0:
                            // space is smaller than file, split file
                            space.id = file.id;
                            file.size = Math.Abs(delta);
                            break;
                        case > 0:
                            // space is larger than file, split space
                            blocks.Insert(i, new(file.id, file.size));
                            space.size = delta;
                            file.id = -1;
                            break;
                    }

                    while (blocks[^1].isFree) {
                        blocks.RemoveAt(blocks.Count - 1);
                    }
                }
            }
        }
        internal void SmartDefrag() {
            var files = blocks
                .Where(b => !b.isFree)
                .OrderByDescending(b => b.id)
                .ToList();

            foreach (var file in files) {
                for (int i = 0; i < blocks.Count; i++) {
                    var space = blocks[i];
                    if (space.id == file.id) {
                        break;
                    }

                    if (space.isFree && space.size >= file.size) {
                        int delta = space.size - file.size;
                        switch (delta) {
                            case 0:
                                space.id = file.id;
                                file.id = -1;
                                break;
                            case < 0:
                                // space is smaller than file, split file
                                space.id = file.id;
                                file.size = Math.Abs(delta);
                                break;
                            case > 0:
                                // space is larger than file, split space
                                blocks.Insert(i, new(file.id, file.size));
                                space.size = delta;
                                file.id = -1;
                                break;
                        }

                        break;
                    }
                }

                for (int i = 0; i < blocks.Count - 1; i++) {
                    if (blocks[i].isFree && blocks[i + 1].isFree) {
                        blocks[i].size += blocks[i + 1].size;
                        blocks.RemoveAt(i + 1);
                    }
                }
            }
        }

        internal long checksum {
            get {
                long i = 0;
                long sum = 0;
                foreach (var block in blocks) {
                    for (int j = 0; j < block.size; j++, i++) {
                        if (!block.isFree) {
                            sum += i * block.id;
                        }
                    }
                }

                return sum;
            }
        }
    }

    internal sealed record Block {
        internal int id;
        internal int size;
        internal bool isFree => id < 0;

        internal Block(int id, int size) {
            this.id = id;
            this.size = size;
        }

        public override string ToString() => $"{id}: {size}";
    }

    internal HDD hdd = new(new FileInput(file).ReadLines().First());

    internal long defragChecksum {
        get {
            hdd.Defrag();

            return hdd.checksum;
        }
    }

    internal long smartDefragChecksum {
        get {
            hdd.SmartDefrag();

            return hdd.checksum;
        }
    }
}