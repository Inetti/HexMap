using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T1, T2> where T1 : Hex where T2 : MapSettings {
        public T1[] AllHexs { get; protected set; }
        protected Dictionary<HexCoordinates, T1> hexDict;

        public  Map(T2 settings) {
            hexDict = new Dictionary<HexCoordinates, T1>();
            AllHexs = CreateMapSectors(settings);
            foreach (var hex in AllHexs) {
                SetHexNeighbors(hex);
                hexDict[hex.Coordinates] = hex;
            }
        }

        /// <summary>
        /// Return array of hexs without neighbors
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected abstract T1[] CreateMapSectors(T2 settings);

        private void SetHexNeighbors(Hex T) {
            foreach (var dir in HexCoordinates.directions) {
                HexCoordinates coordinates = new HexCoordinates(T.Coordinates.X + dir.X, T.Coordinates.Z + dir.Y);
                T1 neighbor = GetHex(coordinates);
                if (neighbor != null) {
                    T.AddNeighbor(neighbor);
                }
            }
        }    

        public T1 GetHex(HexCoordinates coordinates) {
            T1 hex;
            hexDict.TryGetValue(coordinates, out hex);
            return hex;
        }

        /// <summary>
        /// Return hex by offset coordinates
        /// </summary>
        /// <param name="offsetX">X offset</param>
        /// <param name="offsetZ">Z offset</param>
        /// <returns></returns>
        public T1 GetHex(int offsetX, int offsetZ)
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
        public T1[] GetCircle(Hex center, int radius) {
            List<T1> circle = new List<T1>();
            HexCoordinates coordinate = new HexCoordinates(center.Coordinates.X - radius, center.Coordinates.Z + radius);
            T1 hex = GetHex(coordinate);
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