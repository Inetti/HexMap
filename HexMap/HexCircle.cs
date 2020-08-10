using System;
using System.Collections.Generic;

namespace HexMap
{
    public class HexCircle<T> where T: Hex
    {
        private readonly Map<T> map;

        public HexCircle(Map<T> map)
        {
            this.map = map;
        }

        public T[] GetCircle(T center, int radius)
        {
            HexCoordinates[] circleCoordinates = center.Coordinates.GetCircle(radius);
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
