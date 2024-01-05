using Utilities;

namespace Day23;

sealed class Runtime {
    internal int maximumNumberOfSteps => map.width;

    readonly CharacterMap map;

    internal Runtime(string file) {
        map = new FileInput(file).ReadAllAsCharacterMap();
    }
}

static class Extensions {
}