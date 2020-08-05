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
}
