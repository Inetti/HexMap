using System;
using System.Collections.Generic;
using System.Text;
using HexMap;

namespace HexMap.Tests
{
    class TestSquareMap : SquareMap<TestHex>
    {
        public TestSquareMap(int width, int height) : base(width, height)
        {
        }

        protected override TestHex CreateHex(int offsetX, int offsetZ, int id)
        {
            return new TestHex(offsetX, offsetZ, id);
        }
    }

    class TestRoundMap : RoundMap<TestHex>
    {
        public TestRoundMap(int radius) : base(radius)
        {         
        }

        protected override TestHex CreateHex(HexCoordinates coordinates, int id)
        {
            return new TestHex(coordinates, id);
        }
    }
}
