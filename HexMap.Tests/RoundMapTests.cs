using NUnit.Framework;
using System;

namespace HexMap.Tests {
    class RoundMapTests {
        MapTests<Hex> mapTests;
        RoundMap<Hex> map;

        [SetUp]
        public void Setup() {
            Random random = new Random();
            map = new RoundMap<Hex>(random.Next(1, 10));
            mapTests = new MapTests<Hex>(map);
        }

        [Test]
        public void GetAllHex_should_return_not_null() {
            //Assert
            mapTests.GetAllHex_should_return_not_null();
        }

        [Test]
        public void GetAllHex_should_return_array_with_length_equals_map_square() {
            int expectedSizeMap = 1;
            for (int r = 0; r < map.Radius; r++) {
                expectedSizeMap += (r + 1) * 6;
            }

            mapTests.GetAllHex_should_return_array_with_length_equals_map_square(expectedSizeMap);
        }

        [Test]
        public void GetHex_should_return_null_by_wrong_HexCoordinates() {
            //Arrange
            HexCoordinates coordinates = new HexCoordinates(map.Radius + 100, map.Radius + 100);
            mapTests.GetHex_should_return_null_by_wrong_HexCoordinates(coordinates);
        }

        [Test]
        public void GetHex_should_return_null_by_wrong_OffsetCoordinates() {
            //Arrange
            Vector2DInt coordinates = new Vector2DInt(map.Radius + 100, map.Radius + 100);
            mapTests.GetHex_should_return_null_by_wrong_OffsetCoordinates(coordinates);
        }

        [Test]
        public void GetHex_should_return_correct_hex_by_correct_Coordinates() {
            mapTests.GetHex_should_return_correct_hex_by_correct_Coordinates();
        }

        [Test]
        public void GetCircle_should_return_array_with_length_more_then_zero_by_exested_centre_and_correct_radius() {
            mapTests.GetCircle_should_return_array_with_length_more_then_zero_by_exested_centre_and_correct_radius();
        }

        [Test]
        public void GetCircle_should_return_null_by_incorrect_radius() {
            mapTests.GetCircle_should_return_null_by_incorrect_radius(map.Radius + map.Radius + 1);
        }

        [Test]
        public void GetCircle_should_return_null_by_unexisted_center() {
            mapTests.GetCircle_should_return_null_by_unexisted_center();
        }
    }
}
