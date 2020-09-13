using NUnit.Framework;
using System.Collections.Generic;

namespace HexMap.Tests {
    class MapTests<T> where T : Hex {
        private readonly Map<T> map;

        public MapTests(Map<T> map) {
            this.map = map;
        }

        public void GetAllHex_should_return_not_null() {
            //Assert
            Assert.IsNotNull(map.GetAllHex());
        }

        public void GetAllHex_should_return_array_with_length_equals_map_square(int expectedSizeMap) {
            //Act
            var actualSizeMap = map.GetAllHex().Length;

            //Assert
            Assert.AreEqual(expectedSizeMap, actualSizeMap);
        }

        public void GetHex_should_return_null_by_wrong_HexCoordinates(HexCoordinates coordinates) {
            //Act
            var actualHex = map.GetHex(coordinates);

            //Assert
            Assert.IsNull(actualHex);
        }

        public void GetHex_should_return_null_by_wrong_OffsetCoordinates(Vector2DInt coordinates) {
            //Act
            var actualHex = map.GetHex(coordinates.X, coordinates.Y);

            //Assert
            Assert.IsNull(actualHex);
        }

        public void GetHex_should_return_correct_hex_by_correct_Coordinates() {
            foreach (var expectedHex in map.GetAllHex()) {
                //Arrange
                HexCoordinates coordinates = expectedHex.Coordinates;
                Vector3DInt offset = expectedHex.Offset;

                //Act
                var actualHex1 = map.GetHex(coordinates);
                var actualHex2 = map.GetHex(offset.X, offset.Z);

                //Assert
                Assert.AreEqual(expectedHex, actualHex1);
                Assert.AreEqual(expectedHex, actualHex2);
            }
        }

        public void GetCircle_should_return_array_with_length_more_then_zero_by_exested_centre_and_correct_radius() {
            //Assert
            foreach (var hex in map.GetAllHex()) {
                List<T> circle = map.GetCircle(hex, 1);
                Assert.IsTrue(circle.Count > 0);
            }
        }

        public void GetCircle_should_return_list_with_zero_size_by_incorrect_radius(int radius) {
            //arrange
            var expectedValue = true;

            //act
            var acturalValue = true;
            foreach (var hex in map.GetAllHex()) {
                acturalValue &= map.GetCircle(hex, radius).Count == 0;
            }

            //Assert
            Assert.AreEqual(expectedValue, acturalValue);
        }

        public void GetCircle_should_return_list_with_zero_size_by_unexisted_center() {
            T unexistedCenter = new Hex(0, 0, 0) as T;
            Assert.AreEqual(map.GetCircle(unexistedCenter, 1).Count, 0);
        }
    }
}
