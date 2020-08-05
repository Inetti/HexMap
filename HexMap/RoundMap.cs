using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexMap
{
    public abstract class RoundMap<T> : Map<T> where T : Hex
    {
        public int Radius { get; private set; }
        public int Size { get; private set; }
        public Dictionary<HexCoordinates, T> hexsDict;

        private T[] hexs;

        public RoundMap(int radius)
        {
            Radius = radius;
            hexsDict = new Dictionary<HexCoordinates, T>();
            hexs = CreateHexs();
        }

        private T[] CreateHexs()
        {
            List<T> hexsList = new List<T>();
            int hexID = 0;
            HexCoordinates center = new HexCoordinates(0, 0);
            hexsList.Add(CreateNewHex(center, hexID++));
            for (int circle = 1; circle < Radius + 1; circle++)
            {
                HexCoordinates[] hexCoordinates = HexCoordinates.GetCircle(center, circle);
                foreach (var coord in hexCoordinates)
                {
                    hexsList.Add(CreateNewHex(coord, hexID++));
                }
            }
            return hexsList.ToArray();
        }

        private T CreateNewHex(HexCoordinates coordinates, int id)
        {
            T hex = CreateHex(coordinates, id);
            hexsDict.Add(coordinates, hex);
            return hex;
        }

        protected abstract T CreateHex(HexCoordinates coordinates, int id);

        public override T[] GetAllHex()
        {
            return hexs;
        }

        public override T GetHex(HexCoordinates coordinates)
        {
            T hex;
            hexsDict.TryGetValue(coordinates, out hex);
            return hex;
        }

        public override T GetHex(int offsetX, int offsetZ)
        {
            HexCoordinates coordinates = HexCoordinates.FromOffsetCoordinates(offsetX, offsetZ);
            return GetHex(coordinates);
        }
    }
}
