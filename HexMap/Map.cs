using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T> where T : Hex {
        public abstract T[] GetAllHex();

        public abstract T GetHex(HexCoordinates coordinates);

        /// <summary>
        /// Return hex by offset coordinates
        /// </summary>
        /// <param name="offsetX">X offset</param>
        /// <param name="offsetZ">Z offset</param>
        /// <returns></returns>
        public abstract T GetHex(int offsetX, int offsetZ);

        /// <summary>
        /// Return circle of hexs with current radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public T[] GetCircle(T center, int radius) {
            HexCoordinates[] circleCoordinates = HexCoordinates.GetCircle(center.Coordinates, radius);
            List<T> circle = new List<T>();
            foreach (var coordinates in circleCoordinates)
            {
                T hex = GetHex(coordinates);
                if (hex != null && circle.Contains(hex) == false)
                {
                    circle.Add(hex);
                }
            }
            return circle.ToArray();
        }        
    }
}