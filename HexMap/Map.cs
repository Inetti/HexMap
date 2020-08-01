using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T> where T : Hex {
        public T[] AllHexs { get; protected set; }
        protected Dictionary<HexCoordinates, T> hexDict;

        public  Map(MapSettings mapData) {
            hexDict = new Dictionary<HexCoordinates, T>();
            AllHexs = CreateMapSectors(mapData);
            foreach (var hex in AllHexs) {
                SetHexNeighbors(hex);
                hexDict[hex.Coordinates] = hex;
            }
        }

        protected abstract T[] CreateMapSectors(MapSettings mapData);

        private void SetHexNeighbors(Hex T) {
            foreach (var dir in HexCoordinates.directions) {
                HexCoordinates coordinates = new HexCoordinates(T.Coordinates.X + dir.X, T.Coordinates.Z + dir.Y);
                T neighbor = GetHex(coordinates);
                if (neighbor != null) {
                    T.SetNeighbor(neighbor);
                }
            }
        }    

        public T GetHex(HexCoordinates coordinates) {
            if (hexDict.ContainsKey(coordinates)) {
                return hexDict[coordinates];
            }
            return null;
        }

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