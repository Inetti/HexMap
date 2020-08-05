using NUnit.Framework;
using System.Data;

namespace HexMap.Tests
{
    public class Tests
    {
        TestSquareMap squareMap;
        int circleRadius;

        [SetUp]
        public void Setup()        
        {
            squareMap = new TestSquareMap(100, 100);
            circleRadius = 10;
        }

        [Test]
        public void CheckNumberOfRealHexsInMaps()
        {
            TestHex[] hexsOfSquareMap = squareMap.GetAllHex();
            Assert.AreEqual(squareMap.Width * squareMap.Height, hexsOfSquareMap.Length);
            CheckAllHexsAreNotEqualNull(hexsOfSquareMap);
        }

        private void CheckAllHexsAreNotEqualNull<T>(T[] hexs) where T : Hex
        {
            for (int i = 0; i < hexs.Length; i++)
            {
                Assert.AreNotEqual(null, hexs[i]);
            }
        }

        [Test]
        public void GetRightHex()
        {
            GetRightHex(squareMap);
        }

        private void GetRightHex<T>(Map<T> map) where T : Hex
        {
            foreach (var hex in map.GetAllHex())
            {
                Vector3DInt offset = hex.Offset;
                HexCoordinates hexCoordinates = hex.Coordinates;
                T hexTakedByOffset = map.GetHex(offset.X, offset.Z);
                T hexTakedByCoord = map.GetHex(hexCoordinates);
                Assert.AreEqual(hex.ID, hexTakedByOffset.ID, hexTakedByCoord.ID);
            }
        }

        [Test]
        public void EquivalenceOfTypeCoordinate()
        {
            foreach (var hex in squareMap.GetAllHex())
            {
                Vector3DInt offset = hex.Offset;
                HexCoordinates hexCoordinates = HexCoordinates.FromOffsetCoordinates(offset.X, offset.Z);
                Hex hexTest = squareMap.GetHex(hexCoordinates);
                Assert.AreEqual(hex.ID, hexTest.ID);

                hexCoordinates = hex.Coordinates;
                offset = HexCoordinates.OffsetFromHexCoordinates(hexCoordinates);
                hexTest = squareMap.GetHex(offset.X, offset.Z);
                Assert.AreEqual(hex.ID, hexTest.ID);
            }
        }

        [Test]
        public void CheckSizeFullCircle()
        { 
            int offsetX = squareMap.Width / 2;
            int offsetZ = squareMap.Height / 2;
            TestHex center = squareMap.GetHex(offsetX, offsetZ);
            TestHex[] circle = squareMap.GetCircle(center, circleRadius);
            Assert.AreEqual(circle.Length, circleRadius * 6);
            CheckAllHexsAreNotEqualNull(circle);
        }
    }
}