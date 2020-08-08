using System;
using System.Collections.Generic;

namespace HexMap {
    [System.Serializable]
    public enum HexDirection { NE, E, SE, SW, W, NW }

    [System.Serializable]
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
        
        public HexCoordinates(int x, int z) {
            X = x;
            Z = z;
        }

        public override string ToString() {
            return $"x = {X}, y = {Y}, z = {Z}";
        }

        #region STATIC_FUNCTIONS
        public static HexCoordinates FromOffsetCoordinates(int x, int z) {
            return new HexCoordinates(x - z/2, z);
        }

        public static Vector3DInt OffsetFromHexCoordinates(HexCoordinates coordinates) {
            int x = coordinates.X + coordinates.Z / 2;
            int z = coordinates.Z;
            return new Vector3DInt(x, -x - z, z);
        }

        /// <summary>
        /// Return circle of hexCoordinates with current center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static HexCoordinates[] GetCircle(HexCoordinates center, int radius)
        {
            List<HexCoordinates> circle = new List<HexCoordinates>();
            HexCoordinates coordinates = new HexCoordinates(center.X - radius, center.Z + radius);
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
        #endregion
    }
}