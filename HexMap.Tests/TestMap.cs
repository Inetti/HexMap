using System;
using System.Collections.Generic;
using System.Text;
using HexMap;

namespace HexMap.Tests
{
    class TestMap : Map<TestHex, TestSettings>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public TestMap(TestSettings settings) : base(settings)
        {
        }

        protected override TestHex[] CreateMapSectors(TestSettings settings)
        {
            Width = settings.Width;
            Height = settings.Heigth;
            TestHex[] hexs = new TestHex[Width * Height];
            int hexID = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Height; z++)
                {
                    hexs[x + z * Width] = new TestHex(x, z, true, hexID);
                    hexID++;
                }
            }
            return hexs;
        }
    }

    class TestSettings : MapSettings
    {
        public TestSettings(int width, int heigth)
        {
            Width = width;
            Heigth = heigth;
        }

        public int Width { get; private set; }
        public int Heigth { get; private set; }
    }
}
