using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T> where T : Hex {
        protected List<T> hexs;
        public T[] GetAllHex()
        {
            return hexs.ToArray();
        }

        public abstract T GetHex(HexCoordinates coordinates);
        /// <summary>
        /// Return hex by offset coordinates
        /// </summary>
        /// <param name="offsetX">X offset</param>
        /// <param name="offsetZ">Z offset</param>
        /// <returns></returns>
        public abstract T GetHex(int offsetX, int offsetZ);

        /// <summary>
        /// Return circle of hexs with current center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public T[] GetCircle(T center, int radius) {
            if (hexs.Contains(center))
            {
                HexCoordinates[] circleCoordinates = center.Coordinates.GetCircle(radius);
                List<T> circle = new List<T>();
                foreach (var coordinates in circleCoordinates)
                {
                    T hex = GetHex(coordinates);
                    if (hex != null)
                    {
                        circle.Add(hex);
                    }
                }
                if (circle.Count > 0) 
                    return circle.ToArray();
            }
            return null;
        }
    }
}