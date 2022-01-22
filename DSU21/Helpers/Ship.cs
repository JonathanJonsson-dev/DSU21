using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Helpers
{
    public class Ship
    {
        

        public enum Direction
        {
            Horizontal, Vertical
        }
        public Ship(Point startLocation, Direction direction)
        {
            StartLocation = startLocation;
            Direction = direction;
        }

        public Point StartLocation { get; }
        public Direction Direction { get; }
    }
}
