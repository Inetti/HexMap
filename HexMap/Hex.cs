using System;
using System.Collections.Generic;
using Pathfinder;

namespace HexMap
{
    [Serializable]
    public abstract class Hex : INode
    {
        protected List<INode> neighbors;

        public Vector3DInt Offset { get; private set; }
        public HexCoordinates Coordinates { get; private set; }
        public int ID { get; private set; }

        protected int cost;
        public int Cost
        {
            get
            {
                return cost;
            }
        }

        #region CONSTRUCTORS
        public Hex(int x, int z, int id) : this(HexCoordinates.FromOffsetCoordinates(x, z), id) { }
        public abstract bool IsWalkable();

        public Hex(HexCoordinates coordinates, int id)
        {
            Coordinates = coordinates;
            Offset = HexCoordinates.OffsetFromHexCoordinates(coordinates);
            neighbors = new List<INode>();
            ID = id;
        }
        #endregion

        public Hex GetNeighborByDirection(HexDirection direction)
        {
            Vector2DInt dir = HexCoordinates.directions[(int)direction];
            HexCoordinates neighborCoordinates = new HexCoordinates(Coordinates.X + dir.X, Coordinates.Z + dir.Y);
            foreach (Hex hex in neighbors)
            {
                if (hex.Coordinates.X == neighborCoordinates.X && hex.Coordinates.Z == neighborCoordinates.Z)
                {
                    return hex;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return $"Hex(Coord: {Coordinates.X}, {Coordinates.Y}, {Coordinates.Z} | Offset: {Offset.X}, {Offset.Z} )";
        }

        public int HeuristicCostTo(INode n)
        {
            return DistanceTo((Hex)n);
        }

        public int DistanceTo(Hex hex)
        {
            int xy = Math.Max(Math.Abs(Coordinates.X - hex.Coordinates.X), Math.Abs(Coordinates.Y - hex.Coordinates.Y));
            return Math.Max(xy, Math.Abs(Coordinates.Z - hex.Coordinates.Z));
        }


        public void AddNeighbor(INode neighbor)
        {
            if (neighbors.Contains(neighbor))
                return;
            neighbors.Add(neighbor);
            neighbor.AddNeighbor(this);
        }

        public INode[] GetNeighbors()
        {
            return neighbors.ToArray();
        }
    }
}
