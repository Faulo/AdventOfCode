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

        [TestCase(1, 2, 1, 2, 1, 2)]
        [TestCase(0, 0, 10, 20, 10, 20)]
        [TestCase(0, 0, 10, 20, 5, 0)]
        [TestCase(0, 0, 10, 20, 0, 5)]
        [TestCase(9, 4, 10, 6, 10, 5)]
        [TestCase(0, 0, 10, 20, 5, 20)]
        [TestCase(-1, -1, 1, 1, 0, -1)]
        [TestCase(-1, -1, 1, 1, 0, 1)]
        [TestCase(-1, -1, 1, 1, -1, 0)]
        [TestCase(-1, -1, 1, 1, 1, 0)]
        public void GivenPoints_WhenRectangleBorder_ThenReturnBorder(int x1, int y1, int x2, int y2, int expectedX, int expectedY) {
            var start = new Vector2Int(x1, y1);
            var end = new Vector2Int(x2, y2);
            var expected = new Vector2Int(expectedX, expectedY);

            Assert.That(Vector2Int.RectangleBorder(start, end), Does.Contain(expected));
        }
    }
}
