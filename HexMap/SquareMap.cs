using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public class SquareMap<T> : Map<T> where T : Hex {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        
        public SquareMap() {}

        public SquareMap(int width, int heigth) {
            Width = width;
            Height = heigth;
            hexs = new List<T>();
            CreateHexs();
        }
        
        private void CreateHexs() {
            for (int z = 0; z < Height; z++) {
                for (int x = 0; x < Width; x++) {
                    T hex = CreateHex(x, z, x + z * Width);
                    hexs.Add(hex);
                }
            }
        }

        private T CreateHex(int offsetX, int offsetZ, int id) {
            return new Hex(offsetX, offsetZ, id) as T;
        }
        
        public override bool IsValide(HexCoordinates coordinates) {
            Vector3DInt offset = coordinates.ToOffsetCoordinates();
            return offset.X >= 0 && offset.X < Width && offset.Z >= 0 && offset.Z < Height;
        }
        
        public override T GetHex(HexCoordinates coordinates) {
            Vector3DInt offset = coordinates.ToOffsetCoordinates();
            return GetHex(offset.X, offset.Z);
        }

        public override T GetHex(int offsetX, int offsetZ) {
            HexCoordinates coordinates = HexCoordinates.FromOffsetCoordinates(offsetX, offsetZ);
            if (IsValide(coordinates)) {
                return hexs[offsetX + offsetZ * Width];
            }    
            throw new Exception("Wrong coordinate");
        }
    }
}
