using Utilities;

namespace Day08;

sealed partial class Runtime {

    readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file)
            .ReadAllAsCharacterMap();
    }

    internal long antinodeCount {
        get {
            return map
                .allPositionsWithin
                .Count();
        }
    }
}