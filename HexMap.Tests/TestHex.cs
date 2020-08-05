using System;
using System.Collections.Generic;
using System.Text;
using HexMap;
using NUnit.Framework;

namespace HexMap.Tests
{
    internal class TestHex : Hex
    {
        public TestHex(int x, int z, int id) : base(x, z, id)
        {
        }

        public TestHex(HexCoordinates coordinates, int id) : base(coordinates, id)
        {

        }
    }
}
