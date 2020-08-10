using NUnit.Framework;
using System;
using System.Linq;

namespace HexMap.Tests
{
    public class HexCoordinateTests
    {
        Random random;
        [SetUp]
        public void Setup()
        {
            random = new Random();
        }

        [Test]
        public void GetCircle_should_return_array_with_length_equal_six_circle_radius()
        {
            //Arrange
            int radius = random.Next(100);
            HexCoordinates center = new HexCoordinates(random.Next(1000) - random.Next(2000), random.Next(1000) - random.Next(2000));
            int expectLength = radius * 6;

            //Act
            HexCoordinates[] circle = center.GetCircle(radius);
            int actualLength = circle.Length;
            
            //Assert
            Assert.AreEqual(expectLength, actualLength);
        }

        [Test]
        public void GetCircle_should_return_circle()
        {
            //Arrange
            HexCoordinates[] expectedCoord = new[] {
                new HexCoordinates(0, 1),
                new HexCoordinates(1, 0),
                new HexCoordinates(1, -1),
                new HexCoordinates(0, -1),
                new HexCoordinates(-1, 0),
                new HexCoordinates(-1, 1),
            };

            //Act
            HexCoordinates[] actualCoord = new HexCoordinates(0,0).GetCircle(1);

            //Assert
            foreach (var coord in expectedCoord)
            { 
                Assert.IsTrue(actualCoord.Contains(coord));
            }
        }
    }
}