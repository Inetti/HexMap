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
            List<T> circle = new List<T>();
            HexCoordinates coordinate = new HexCoordinates(center.Coordinates.X - radius, center.Coordinates.Z + radius);
            T hex = GetHex(coordinate);
            if (hex != null) {
                circle.Add(hex);
            }

            for (int x = 1; x < HexCoordinates.directions.Length + 1; x++) {
                Vector2DInt dir = HexCoordinates.directions[x % HexCoordinates.directions.Length];
                for (int i = 0; i < radius; i++) {
                    coordinate = new HexCoordinates(coordinate.X + dir.X, coordinate.Z + dir.Y);
                    hex = GetHex(coordinate);
                    if (hex != null && !circle.Contains(hex)) {
                        circle.Add(hex);
                    }
                }
            }
            return circle.ToArray();
        }
    }
}