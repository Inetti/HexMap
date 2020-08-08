using System;
using System.Collections.Generic;

namespace HexMap {
    [Serializable]
    public abstract class Map<T> where T : Hex {
        private HexCircleManager<T> circleManager;

        public abstract T[] GetAllHex();
        public abstract T GetHex(HexCoordinates coordinates);
        /// <summary>
        /// Return hex by offset coordinates
        /// </summary>
        /// <param name="offsetX">X offset</param>
        /// <param name="offsetZ">Z offset</param>
        /// <returns></returns>
        public abstract T GetHex(int offsetX, int offsetZ);

        /// <summary>
        /// Return circle of hexs with current center and radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public T[] GetCircle(T center, int radius) {
            if (circleManager == null)
            {
                circleManager = new HexCircleManager<T>(this);
            }
            return circleManager.GetCircle(center, radius);
        }
    }
}