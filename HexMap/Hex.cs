using System;
using System.Collections.Generic;
using Pathfinder;

namespace HexMap {
    [Serializable]
    public abstract class Hex : INode {
        protected List<INode> neighbors;

        public Vector3DInt Offset { get; private set; }
        public HexCoordinates Coordinates { get; private set; }
        private int id = -1;
        public int ID {
            get {
                return id;
            }
            set {
                if (id == -1) {
                    id = value;
                }
            }
        }

        protected int cost;
        public int Cost
        {
            get
            {
                return cost;
            }
        }

        #region CONSTRUCTORS
        public Hex(int x, int z) : this(HexCoordinates.FromOffsetCoordinates(x, z)) { }

        public Hex(HexCoordinates coordinates) {
            Coordinates = coordinates;
            Offset = HexCoordinates.OffsetFromHexCoordinates(coordinates);
            neighbors = new List<INode>();
        }
        #endregion

        public Hex GetNeighborByDirection(HexDirection direction) {
            Vector2DInt dir = HexCoordinates.directions[(int)direction];
            HexCoordinates neighborCoordinates = new HexCoordinates((int)(Coordinates.X + dir.X), (int)(Coordinates.Z + dir.Y));
            foreach (Hex hex in neighbors) {
                if(hex.Coordinates.X == neighborCoordinates.X && hex.Coordinates.Z == neighborCoordinates.Z) {
                    return hex;
                }
            }
            return null;
        }
        
        public override string ToString() {
            return $"Hex(Coord: {Coordinates.X}, {Coordinates.Y}, {Coordinates.Z} | Offset: {Offset.X}, {Offset.Z} )";
        }

        public int HeuristicCostTo(INode n) {
            return DistanceTo((Hex)n); 
        }

        public int DistanceTo(Hex hex) {
            int xy = Math.Max(Math.Abs(Coordinates.X - hex.Coordinates.X), Math.Abs(Coordinates.Y - hex.Coordinates.Y));
            return Math.Max(xy, Math.Abs(Coordinates.Z - hex.Coordinates.Z));
        }

        public virtual bool IsWalkable()
        {
            return true;
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
