using NUnit.Framework;
using System;

namespace HexMap.Tests
{
    public class HexCoordinatesTests
    {
        TestMap map;

        [SetUp]
        public void Setup()        
        {
            map = new TestMap(new TestSettings(1000, 1000));
        }


        [Test]
        [Category("SlowTest")]
        public void EquivalenceOfTypesCoordinate()
        {
            foreach (var hex in map.AllHexs)
            {
                Vector3DInt offset = hex.Offset;
                HexCoordinates hexCoordinates = HexCoordinates.FromOffsetCoordinates(offset.X, offset.Z);
                Hex hexTest = map.GetHex(hexCoordinates);
                Assert.AreEqual(hex.ID, hexTest.ID);
            }

            foreach (var hex in map.AllHexs)
            {
                HexCoordinates hexCoordinates = hex.Coordinates;
                Vector3DInt offset = HexCoordinates.OffsetFromHexCoordinates(hexCoordinates);
                Hex hexTest = map.GetHex(offset.X, offset.Z);
                Assert.AreEqual(hex.ID, hexTest.ID);
            }
        }
    }

    public class MapTests
    {
        TestMap map;
        [SetUp]
        public void Setup()
        {
            map = new TestMap(new TestSettings(10, 10));
        }


    }
}