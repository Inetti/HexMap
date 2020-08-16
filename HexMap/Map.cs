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
                List<T> circle = new List<T>();
                HexCoordinates[] circleCoordinates = center.Coordinates.GetCircle(radius);
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

        /// <summary>
        /// Return areay of hexs with current center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public T[] GetArea(T center, int radius)
        {
            if (hexs.Contains(center))
            {
                List<T> area = new List<T>();
                for (int r = 1; r < radius + 1; r++)
                {
                    T[] circle = GetCircle(center, r);
                    if (circle != null)
                        area.AddRange(circle);
                }
                if (area.Count > 0)
                    return area.ToArray();
            }
            return null;
        }
    }
}