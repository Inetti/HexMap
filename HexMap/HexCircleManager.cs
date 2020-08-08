using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexMap
{
    public class HexCircleManager<T> where T: Hex
    {
        private Map<T> map;

        public HexCircleManager(Map<T> map)
        {
            this.map = map;
        }

        public T[] GetCircle(T center, int radius)
        {
            HexCoordinates[] circleCoordinates = HexCoordinates.GetCircle(center.Coordinates, radius);
            List<T> circle = new List<T>();
            foreach (var coordinates in circleCoordinates)
            {
                T hex = map.GetHex(coordinates);
                if (hex != null)
                {
                    circle.Add(hex);
                }
            }
            return circle.ToArray();
        }
    }
}
