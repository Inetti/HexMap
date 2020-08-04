using System;

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
        
        public static HexCoordinates FromOffsetCoordinates(int x, int z) {
            return new HexCoordinates(x - z/2, z);
        }

        public static Vector3DInt OffsetFromHexCoordinates(HexCoordinates coordinates) {
            int x = coordinates.X + coordinates.Z / 2;
            int z = coordinates.Z;
            return new Vector3DInt(x, -x - z, z);
        }

        public static HexCoordinates FromWorldPosition(float x, float z)
        {
            x /= (HexMetrics.InnerRadius * 2f);
            float y = -x;
            float offset = z / (HexMetrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            int iX = (int)Math.Round(x);
            int iY = (int)Math.Round(y);
            int iZ = (int)Math.Round(-x - y);

            if (iX + iY + iZ != 0) {
                float dX = Math.Abs(x - iX);
                float dY = Math.Abs(y - iY);
                float dZ = Math.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ) {
                    iX = -iY - iZ;
                } else if (dZ > dY) {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinates(iX, iZ);
        }

        public override string ToString() {
            return $"x = {X}, y = {Y}, z = {Z}";
        }
    }
}