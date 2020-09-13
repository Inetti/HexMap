using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T> where T : Hex {
        protected List<T> hexs;
        private T[] hexsArray;
        public T[] GetAllHex() {
            if (hexsArray == null) {
                hexsArray = hexs.ToArray();
            }
            return hexsArray;
        }

        public abstract bool IsValide(HexCoordinates coordinates);
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
        public List<T> GetCircle(T center, int radius) {
            List<T> circle = new List<T>();
            if (hexs.Contains(center)) {
                HexCoordinates[] circleCoordinates = center.Coordinates.GetCircle(radius);
                foreach (var coordinates in circleCoordinates) {
                    if (IsValide(coordinates)) {
                        T hex = GetHex(coordinates);
                        if (hex != null) {
                            circle.Add(hex);
                        }
                    }
                }
            }
            return circle;
        }

        /// <summary>
        /// Return area of hexs with current center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public List<T> GetArea(T center, int radius) {
            List<T> area = new List<T>();
            if (hexs.Contains(center)) {
                for (int r = 1; r < radius + 1; r++) {
                    List<T> circle = GetCircle(center, r);
                    foreach (var hex in circle) {
                        area.Add(hex);
                    }
                }
            }
            return area;
        }
    }
}