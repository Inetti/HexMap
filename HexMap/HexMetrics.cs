namespace HexMap
{
    public static class HexMetrics
    {
        static float outerRadius = 1;
        public static float OuterRadius
        {
            get { return outerRadius > 0 ? outerRadius : 1; }
            set { outerRadius = value; }
        }

        public static float InnerRadius
        {
            get { return OuterRadius * 0.8660254037844386f; }
        }

        public static float Height { get { return 2 * OuterRadius; } }
        public static float Width { get { return 2 * InnerRadius; } }
        public static float VerticalDistance { get { return Height * 0.75f; } }
        public static float HorizontalDistance { get { return Width; } }

        public static Vector3D[] corners = {
            new Vector3D(0f, 0f, OuterRadius),
            new Vector3D(InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3D(InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3D(0f, 0f, -OuterRadius),
            new Vector3D(-InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3D(-InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3D(0f, 0f, OuterRadius),
        };

        public static float GetPositionX(Hex hex)
        {
            float xOffset = (hex.Offset.Z % 2) * (Width / 2f);
            return hex.Offset.X * HorizontalDistance + xOffset;
        }

        public static float GetPositionZ(Hex hex)
        {
            return hex.Offset.Z * VerticalDistance;
        }

        public static Vector3D GetPosition(Hex hex)
        {
            float x = GetPositionX(hex);
            float z = GetPositionZ(hex);
            return new Vector3D(x, 0, z);
        }
    }
}