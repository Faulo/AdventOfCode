using NUnit.Framework;

namespace Utilities {
    class Tests {
        [TestCase(1, 1)]
        [TestCase(10, 20)]
        public void GivenCharacterMap_WhenAccessWidth_ThenReturnFirst(int width, int height) {
            var sut = new CharacterMap(new char[width, height]);

            Assert.That(sut.width, Is.EqualTo(width));
        }

        [TestCase(1, 1)]
        [TestCase(10, 20)]
        public void GivenCharacterMap_WhenAccessHeight_ThenReturnSecond(int width, int height) {
            var sut = new CharacterMap(new char[width, height]);

            Assert.That(sut.height, Is.EqualTo(height));
        }

        [TestCase(0, 0)]
        [TestCase(4, 4)]
        [TestCase(4, 9)]
        public void GivenCharacterMap_WhenAccessAllPositionsWithin_ThenContain(int x, int y) {
            var sut = new CharacterMap(new char[5, 10]);

            Assert.That(sut.allPositionsWithin, Contains.Item(new Vector2Int(x, y)));
        }

        [TestCase(0, 0)]
        [TestCase(4, 4)]
        [TestCase(4, 9)]
        public void GivenCharacterMap_WhenAccessAllPositionsAndCharactersWithin_ThenContain(int x, int y) {
            var sut = new CharacterMap(new char[5, 10]);

            Assert.That(sut.allPositionsAndCharactersWithin, Contains.Item((new Vector2Int(x, y), default(char))));
        }

        [TestCase(0, 0, '1')]
        [TestCase(4, 9, '2')]
        public void GivenCharacterMap_WhenSerializeDeserialize_ThenIsSame(int x, int y, char expected) {
            var sut = new CharacterMap(new char[5, 10]);
            sut[x, y] = expected;

            string serialization = sut.Serialize();

            sut = new CharacterMap(new char[5, 10]);
            sut.Deserialize(serialization);

            Assert.That(sut[x, y], Is.EqualTo(expected));
        }
    }
}
