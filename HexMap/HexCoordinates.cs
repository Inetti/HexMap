using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public enum HexDirection { NE, E, SE, SW, W, NW }

    [Serializable]
    public struct HexCoordinates {
        public static readonly Vector2DInt[] directions = new Vector2DInt[] { new Vector2DInt(0, 1), new Vector2DInt(1, 0), new Vector2DInt(1, -1),
                                                        new Vector2DInt(0, -1), new Vector2DInt(-1, 0), new Vector2DInt(-1, 1)};

        public static Vector2DInt GetVectorByDirection(HexDirection direction)
        {
            return directions[(int)direction];
        }

        public int X { get; private set; }
        public int Z { get; private set; }
        public int Y { get { return -X - Z; } }
        
        public HexCoordinates(int x, int z) : this() {
            X = x;
            Z = z;
        }

        /// <summary>
        /// Return circle of hexCoordinates with center in these coordinates
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public HexCoordinates[] GetCircle(int radius)
        {
            List<HexCoordinates> circle = new List<HexCoordinates>();
            HexCoordinates coordinates = new HexCoordinates(X - radius, Z + radius);
            for (int x = 1; x < directions.Length + 1; x++)
            {
                Vector2DInt dir = directions[x % directions.Length];
                for (int i = 0; i < radius; i++)
                {
                    coordinates = new HexCoordinates(coordinates.X + dir.X, coordinates.Z + dir.Y);
                    circle.Add(coordinates);
                }
            }
            return circle.ToArray();
        }

        public Vector3DInt ToOffsetCoordinates()
        {
            int x = X + Z / 2;
            int z = Z;
            return new Vector3DInt(x, -x - z, z);
        }

        public override string ToString()
        {
            return string.Format("x = {0}, y = {1}, z = {2}", X, Y, Z);
        }

        #region STATIC_FUNCTIONS
        public static HexCoordinates FromOffsetCoordinates(int x, int z) {
            return new HexCoordinates(x - z/2, z);
        }
        #endregion
    }
}