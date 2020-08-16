using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public class SquareMap<T> : Map<T> where T : Hex {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SquareMap(int width, int heigth) {
            Width = width;
            Height = heigth;
            hexs = new List<T>();
            int hexID = 0;
            for (int z = 0; z < Height; z++) {
                for (int x = 0; x < Width; x++) {
                    T hex = CreateHex(x, z, hexID);
                    hexs.Add(hex);
                    hexID++;
                }
            }
        }

        protected virtual T CreateHex(int offsetX, int offsetZ, int id) {
            return new Hex(offsetX, offsetZ, id) as T;
        }

        public override T GetHex(HexCoordinates coordinates) {
            Vector3DInt offset = coordinates.ToOffsetCoordinates();
            return GetHex(offset.X, offset.Z);
        }

        public override T GetHex(int offsetX, int offsetZ) {
            if (offsetX < 0 || offsetX >= Width || offsetZ < 0 || offsetZ >= Height)
                return null;
            return hexs[offsetX + offsetZ * Width];
        }
    }
}
