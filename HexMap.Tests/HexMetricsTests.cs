using AutoFixture;
using NUnit.Framework;
using System;

namespace HexMap.Tests {
    class HexMetricsTests {
        HexMetrics hexMetrics;
        readonly Hex hex;

        [SetUp]
        public void Setup() {
            hexMetrics = new Fixture().Create<HexMetrics>();
            Hex hex = new Fixture().Create<Hex>();
        }

        [Test]
        public void GetHexCoordinateFromWorldPosition_should_return_right_hexCoordinates_for_hex_by_world_coordinates_of_this_hex() {
            Vector3D hexPos = hexMetrics.GetPosition(hex);

            HexCoordinates expectedCoordinates = hex.Coordinates;
            HexCoordinates actualCootdinates = hexMetrics.GetHexCoordinateFromWorldPosition(hexPos.X, hexPos.Z);

            Assert.AreEqual(expectedCoordinates.X, actualCootdinates.X);
            Assert.AreEqual(expectedCoordinates.Z, actualCootdinates.Z);
        }

        [Test]
        public void GetHexCoordinateFromWorldPosition_should_return_right_hexCoordinates_for_hex_by_world_coordinates_in_innerRadius_of_this_hex() {
            Vector3D hexPos = hexMetrics.GetPosition(hex);
            float xPos = (float)(Math.Cos(0.999)) * hexMetrics.InnerRadius;
            float zPos = (float)(Math.Sin(0.999)) * hexMetrics.InnerRadius;
            Vector3D offset = new Vector3D(xPos, hexPos.Y, zPos);

            HexCoordinates expectedCoordinates = hex.Coordinates;
            HexCoordinates actualCootdinates = hexMetrics.GetHexCoordinateFromWorldPosition(hexPos.X + offset.X, hexPos.Z + offset.Z);

            Assert.AreEqual(expectedCoordinates.X, actualCootdinates.X);
            Assert.AreEqual(expectedCoordinates.Z, actualCootdinates.Z);
        }
    }
}
