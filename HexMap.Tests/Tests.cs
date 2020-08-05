using NUnit.Framework;
using System;
using System.Data;

namespace HexMap.Tests
{
    public class MapTests
    {
        TestSquareMap squareMap;
        TestRoundMap roundMap;

        [SetUp]
        public void Setup()        
        {
            squareMap = new TestSquareMap(100, 100);
            roundMap = new TestRoundMap(100);
        }

        [Test]
        public void CheckNumberOfRealHexsInMaps()
        {
            TestHex[] hexs = squareMap.GetAllHex();
            CheckAllHexsAreNotEqualNull(hexs);
            Assert.AreEqual(squareMap.Width * squareMap.Height, hexs.Length);

            hexs = roundMap.GetAllHex();
            CheckAllHexsAreNotEqualNull(hexs);
            int size = 1;
            for (int r = 1; r < roundMap.Radius  + 1; r++)
            {
                size += r * 6;
            }
            Assert.AreEqual(size, hexs.Length);
        }

        private void CheckAllHexsAreNotEqualNull<T>(T[] hexs) where T : Hex
        {
            for (int i = 0; i < hexs.Length; i++)
            {
                Assert.AreNotEqual(null, hexs[i]);
            }
        }

        [Test]
        public void GetRightHexByCoordinates()
        {
            GetRightHex(squareMap);
            GetRightHex(roundMap);
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
    }

    public class HexCoordinateTests
    {
        Random random;
        [SetUp]
        public void Setup()
        {
            random = new Random();
        }

        [Test]
        public void CheckSizeCircle()
        {
            for (int i = 0; i < 1000; i++)
            {
                int radius = random.Next(1000);
                HexCoordinates center = new HexCoordinates(random.Next(1000) - random.Next(2000), random.Next(1000) - random.Next(2000));
                HexCoordinates[] circle = HexCoordinates.GetCircle(center, radius);
                Assert.AreEqual(radius * 6, circle.Length);
            }
        }
        [Test]
        public void EquivalenceOfTypeCoordinate()
        {
            for (int i = 0; i < 1000; i++)
            {
                int x = random.Next(1000) - random.Next(2000);
                int z = random.Next(1000) - random.Next(2000);
                Vector3DInt offset = new Vector3DInt(x, -x - z, z);
                HexCoordinates hexCoordinates = HexCoordinates.FromOffsetCoordinates(offset.X, offset.Z);
                Vector3DInt testOffset = HexCoordinates.OffsetFromHexCoordinates(hexCoordinates);
                Assert.AreEqual(offset.X, testOffset.X);
                Assert.AreEqual(offset.Y, testOffset.Y);
                Assert.AreEqual(offset.Z, testOffset.Z);

                hexCoordinates = new HexCoordinates(x, z);
                offset = HexCoordinates.OffsetFromHexCoordinates(hexCoordinates);
                HexCoordinates testCoord = HexCoordinates.FromOffsetCoordinates(offset.X, offset.Z);
                Assert.AreEqual(hexCoordinates.X, testCoord.X);
                Assert.AreEqual(hexCoordinates.Y, testCoord.Y);
                Assert.AreEqual(hexCoordinates.Z, testCoord.Z);
            }
        }
    }
}