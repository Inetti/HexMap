using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexMap
{
    [Serializable]
    public abstract class SquareMap<T> : Map<T> where T : Hex
    {
        private readonly T[] hexs;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public SquareMap(int width, int heigth)
        {
            Width = width;
            Height = heigth;
            hexs = new T[Width * Height];
            int hexID = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Height; z++)
                {
                    T hex = CreateHex(x, z, hexID);
                    hexs[x + z * Width] = hex;
                    hexID++;
                    foreach (var dir in HexCoordinates.directions)
                    {
                        HexCoordinates coordinates = new HexCoordinates(hex.Coordinates.X + dir.X, hex.Coordinates.Z + dir.Y);
                        T neighbor = GetHex(coordinates);
                        if (neighbor != null)
                            hex.AddNeighbor(neighbor);
                    }
                }
            }
        }

        protected abstract T CreateHex(int offsetX, int offsetY, int id);

        public override T[] GetAllHex()
        {
            return hexs;
        }

        public override T GetHex(HexCoordinates coordinates)
        {
            Vector3DInt offset = HexCoordinates.OffsetFromHexCoordinates(coordinates);
            return GetHex(offset.X, offset.Z);
        }

        public override T GetHex(int offsetX, int offsetZ)
        {
            if (offsetX < 0 || offsetX >= Width || offsetZ < 0 || offsetZ >= Height)
                return null;
            return hexs[offsetX + offsetZ * Width];
        }
    }
}
