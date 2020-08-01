using System;

namespace HexMap {
    [Serializable]
    public struct Vector2DInt {
        public Vector2DInt(int x, int y) : this() {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }


    [Serializable]
    public struct Vector3D {
        public Vector3D(float x, float y, float z) : this() {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
    }

    [Serializable]
    public struct Vector3DInt {
        public Vector3DInt(int x, int y, int z) : this() {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
    }
}