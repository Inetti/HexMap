using System;
using System.Collections.Generic;
using System.Text;
using HexMap;
using NUnit.Framework;

namespace HexMap.Tests
{
    internal class TestHex : Hex
    {
        private bool isWalkable;

        public TestHex(int x, int z, bool isWalkable, int id) : base(x, z, id)
        {
            this.isWalkable = isWalkable;
        }

        public override bool IsWalkable()
        {
            return isWalkable;
        }
    }
}
