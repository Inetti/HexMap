using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexMap.Tests
{
    class RoundMapTests
    {
        RoundMap<Hex> map;

        [SetUp]
        public void Setup()
        {
            Random random = new Random();

            map = new RoundMap<Hex>(random.Next(1, 10));
        }

        [Test]
        public void GetAllHex_should_return_not_null()
        {
            //Assert
            Assert.IsNotNull(map.GetAllHex());
        }

        [Test]
        public void GetHex_should_return_array_with_length_equals_map_square()
        {
            //Arrange
            var expectedSizeMap = 1;
            for (int r = 1; r < map.Radius + 1; r++)
            {
                expectedSizeMap += 6 * r;
            }

            //Act
            var actualSizeMap = map.GetAllHex().Length;

            //Assert
            Assert.AreEqual(expectedSizeMap, actualSizeMap);
        }

        [Test]
        public void GetHex_should_return_null_by_wrong_HexCoordinates()
        {
            //Arrange
            HexCoordinates coordinates = new HexCoordinates(map.Radius + 100, map.Radius + 100);

            //Act
            var actualHex = map.GetHex(coordinates);

            //Assert
            Assert.IsNull(actualHex);
        }

        [Test]
        public void GetHex_should_return_null_by_wrong_OffsetCoordinates()
        {
            //Arrange
            Vector2DInt coordinates = new Vector2DInt(map.Radius + 100, map.Radius + 100);

            //Act
            var actualHex = map.GetHex(coordinates.X, coordinates.Y);

            //Assert
            Assert.IsNull(actualHex);
        }

        [Test]
        public void GetHex_should_return_correct_hex_by_correct_Coordinates()
        {
            foreach (var expectedHex in map.GetAllHex())
            {
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
    }
}
