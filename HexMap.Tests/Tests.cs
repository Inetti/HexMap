using NUnit.Framework;

namespace HexMap.Tests
{
    public class HexCoordinatesTests
    {
        TestMap map;

        [SetUp]
        public void Setup()        
        {
            map = new TestMap(1000, 1000);
        }

        [Test]
        public void EquivalenceOfTypeCoordinate()
        {
            foreach (var hex in map.GetAllHex())
            {
                Vector3DInt offset = hex.Offset;
                HexCoordinates hexCoordinates = HexCoordinates.FromOffsetCoordinates(offset.X, offset.Z);
                Hex hexTest = map.GetHex(hexCoordinates);
                Assert.AreEqual(hex.ID, hexTest.ID);

                hexCoordinates = hex.Coordinates;
                offset = HexCoordinates.OffsetFromHexCoordinates(hexCoordinates);
                hexTest = map.GetHex(offset.X, offset.Z);
                Assert.AreEqual(hex.ID, hexTest.ID);
            }
        }
    }
}