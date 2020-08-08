using AutoFixture;
using Moq;
using NUnit.Framework;
using HexMap;

namespace HexMap.Tests
{
    public class HexCircleManagerTests
    {
        HexCircleManager<Hex> circleManager;
        Map<Hex> map;
        [SetUp]
        public void Setup()
        {
            var mapMock = new Moq.Mock<Map<Hex>>();
            mapMock.Setup(x => x.GetAllHex()).Returns(
                new[] {
                    new Hex(new HexCoordinates(0, 0), 0),
                    new Hex(new HexCoordinates(0, 1), 1),
                    new Hex(new HexCoordinates(1, 0), 2),
                    new Hex(new HexCoordinates(1, -1), 3),
                    new Hex(new HexCoordinates(0, -1), 4),
                    new Hex(new HexCoordinates(-1, 0), 5),
                    new Hex(new HexCoordinates(-1, -1), 6),
                    }
                );
            map = mapMock.Object;

            circleManager = new HexCircleManager<Hex>(map);
        }

        [Test]
        public void GetCircle_should_return_not_null_by_exested_centre_and_correct_radius()
        { 
            //Assert
            foreach (var hex in map.GetAllHex())
            {
                Assert.IsNotNull(circleManager.GetCircle(hex, 1));
            }
        }
    }
}
