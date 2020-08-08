using System;
using System.Collections.Generic;

namespace HexMap
{
    [Serializable]
    public class Hex 
    {
        public Vector3DInt Offset { get; private set; }
        public HexCoordinates Coordinates { get; private set; }
        public int ID { get; private set; }

        #region CONSTRUCTORS
        public Hex(int x, int z, int id) : this(HexCoordinates.FromOffsetCoordinates(x, z), id) { }

        public Hex(HexCoordinates coordinates, int id)
        {
            Coordinates = coordinates;
            Offset = HexCoordinates.OffsetFromHexCoordinates(coordinates);
            ID = id;
        }
        #endregion

        public int DistanceTo(Hex hex)
        {
            int xy = Math.Max(Math.Abs(Coordinates.X - hex.Coordinates.X), Math.Abs(Coordinates.Y - hex.Coordinates.Y));
            return Math.Max(xy, Math.Abs(Coordinates.Z - hex.Coordinates.Z));
        }
    }
}
