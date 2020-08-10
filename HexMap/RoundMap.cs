﻿using System.Collections.Generic;

namespace HexMap
{
    public class RoundMap<T> : Map<T> where T : Hex
    {
        public int Radius { get; private set; }

        private readonly Dictionary<HexCoordinates, T> hexsDict;

        public RoundMap(int radius)
        {
            Radius = radius;
            hexsDict = new Dictionary<HexCoordinates, T>();
            hexs = new List<T>();
            CreateHexs();
        }

        private void CreateHexs()
        {
            int hexID = 0;
            HexCoordinates center = new HexCoordinates(0, 0);
            CreateNewHex(center, hexID++);
            for (int circle = 1; circle < Radius + 1; circle++)
            {
                HexCoordinates[] hexCoordinates = center.GetCircle(circle);
                foreach (var coord in hexCoordinates)
                {
                    CreateNewHex(coord, hexID++);
                }
            }
        }

        private void CreateNewHex(HexCoordinates coordinates, int id)
        {
            T hex = CreateHex(coordinates, id);
            hexsDict.Add(coordinates, hex);
            hexs.Add(hex);
        }

        protected virtual T CreateHex(HexCoordinates coordinates, int id)
        {
            return new Hex(coordinates, id) as T;
        }

        public override T GetHex(HexCoordinates coordinates)
        {
            hexsDict.TryGetValue(coordinates, out T hex);
            return hex;
        }

        public override T GetHex(int offsetX, int offsetZ)
        {
            HexCoordinates coordinates = HexCoordinates.FromOffsetCoordinates(offsetX, offsetZ);
            return GetHex(coordinates);
        }
    }
}
