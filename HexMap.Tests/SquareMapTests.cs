using NUnit.Framework;
using System;

namespace HexMap.Tests
{
    class SquareMapTests
    {
        SquareMap<Hex> map;
        MapTests<Hex> mapTests;

        [SetUp]
        public void Setup()
        {
            Random random = new Random();
            map = new SquareMap<Hex>(random.Next(2, 100), random.Next(2, 100));
            mapTests = new MapTests<Hex>(map);
        }

        [Test]
        public void GetAllHex_should_return_not_null()
        {
            mapTests.GetAllHex_should_return_not_null();
        }
        [Test]
        public void GetAllHex_should_return_array_with_length_equals_map_square()
        {
            //Arrange
            var expectedSizeMap = map.Width * map.Height;
            mapTests.GetAllHex_should_return_array_with_length_equals_map_square(expectedSizeMap);
        }

        [Test]
        public void GetHex_should_return_null_by_wrong_HexCoordinates()
        {
            //Arrange
            HexCoordinates coordinates = new HexCoordinates(map.Width + 100, map.Height + 100);
            mapTests.GetHex_should_return_null_by_wrong_HexCoordinates(coordinates);
        }

        [Test]
        public void GetHex_should_return_null_by_wrong_OffsetCoordinates()
        {
            //Arrange
            Vector2DInt coordinates = new Vector2DInt(map.Width + 100, map.Height + 100);
            mapTests.GetHex_should_return_null_by_wrong_OffsetCoordinates(coordinates);
        }

        [Test]
        public void GetHex_should_return_correct_hex_by_correct_Coordinates()
        {
            mapTests.GetHex_should_return_correct_hex_by_correct_Coordinates();
        }

        [Test]
        public void GetCircle_should_return_array_with_length_more_then_zero_by_exested_centre_and_correct_radius()
        {
            mapTests.GetCircle_should_return_array_with_length_more_then_zero_by_exested_centre_and_correct_radius();
        }

        [Test]
        public void GetCircle_should_return_null_by_incorrect_radius()
        {
            mapTests.GetCircle_should_return_null_by_incorrect_radius(map.Height + map.Width);
        }

        [Test]
        public void GetCircle_should_return_null_by_unexisted_center()
        { 
            mapTests.GetCircle_should_return_null_by_unexisted_center();
        }
    }
}
