using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T> where T : Hex {
        public T[] AllHexs { get; protected set; }
        protected Dictionary<HexCoordinates, T> hexDict;

        public  Map(MapSettings settings) {
            hexDict = new Dictionary<HexCoordinates, T>();
            AllHexs = CreateMapSectors(settings);
            foreach (var hex in AllHexs) {
                SetHexNeighbors(hex);
                hexDict[hex.Coordinates] = hex;
            }
        }

        protected abstract T[] CreateMapSectors(MapSettings settings);

        private void SetHexNeighbors(Hex T) {
            foreach (var dir in HexCoordinates.directions) {
                HexCoordinates coordinates = new HexCoordinates(T.Coordinates.X + dir.X, T.Coordinates.Z + dir.Y);
                T neighbor = GetHex(coordinates);
                if (neighbor != null) {
                    T.AddNeighbor(neighbor);
                }
            }
        }    

        public T GetHex(HexCoordinates coordinates) {
            if (hexDict.ContainsKey(coordinates)) {
                return hexDict[coordinates];
            }
            return null;
        }

        /// <summary>
        /// Return hex by offset coordinates
        /// </summary>
        /// <param name="offsetX">X offset</param>
        /// <param name="offsetZ">Z offset</param>
        /// <returns></returns>
        public T GetHex(int offsetX, int offsetZ)
        {
            HexCoordinates coordinates = HexCoordinates.FromOffsetCoordinates(offsetX, offsetZ);
            if (hexDict.ContainsKey(coordinates))
            {
                return hexDict[coordinates];
            }
            return null;
        }

        /// <summary>
        /// Return circle of hexs with current radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public T[] GetCircle(Hex center, int radius) {
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

    [Serializable]
    public abstract class MapSettings {

    }
}