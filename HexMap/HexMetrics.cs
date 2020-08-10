using System;

namespace HexMap
{
    [Serializable]
    public class HexMetrics
    {
        private float outerRadius;
        public float OuterRadius
        {
            get { return outerRadius; }
            set 
            { 
                if (outerRadius == 0)
                    outerRadius = value; 
            }
        }
        public float InnerRadius { get { return OuterRadius * 0.8660254037844386f; } }
        public float Height { get { return 2 * OuterRadius; } }
        public float Width { get { return 2 * InnerRadius; } }
        public float VerticalDistance { get { return Height * 0.75f; } }
        public float HorizontalDistance { get { return Width; } }

        private Vector3D[] corners;
        public Vector3D[] HexCorners
        { 
            get { return corners; }
            set
            {
                if (corners == null)
                    corners = value;
            }
        }
        
        public HexMetrics(float outerRadius)
        {
            this.outerRadius = outerRadius;
            corners = new [] {
                new Vector3D(0f, 0f, OuterRadius),
                new Vector3D(InnerRadius, 0f, 0.5f * OuterRadius),
                new Vector3D(InnerRadius, 0f, -0.5f * OuterRadius),
                new Vector3D(0f, 0f, -OuterRadius),
                new Vector3D(-InnerRadius, 0f, -0.5f * OuterRadius),
                new Vector3D(-InnerRadius, 0f, 0.5f * OuterRadius),
            };
        }
        
        public float GetPositionX(Hex hex)
        {
            float xOffset = (hex.Offset.Z % 2) * (Width / 2f);
            return hex.Offset.X * HorizontalDistance + xOffset;
        }

        public float GetPositionZ(Hex hex)
        {
            return hex.Offset.Z * VerticalDistance;
        }

        public Vector3D GetPosition(Hex hex)
        {
            float x = GetPositionX(hex);
            float z = GetPositionZ(hex);
            return new Vector3D(x, 0, z);
        }

        public HexCoordinates GetHexCoordinateFromWorldPosition(float x, float z)
        {
            x /= (InnerRadius * 2f);
            float y      = -x;
            float offset = z / (OuterRadius * 3f);
            x -= offset;
            y -= offset;

            int iX = (int)Math.Round(x);
            int iY = (int)Math.Round(y);
            int iZ = (int)Math.Round(-x - y);

            if (iX + iY + iZ != 0)
            {
                float dX = Math.Abs(x - iX);
                float dY = Math.Abs(y - iY);
                float dZ = Math.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ)
                {
                    iX = -iY - iZ;
                }
                else if (dZ > dY)
                {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinates(iX, iZ);
        }
    }
}