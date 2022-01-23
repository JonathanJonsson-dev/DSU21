using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Helpers
{
    public class Ship
    {
        public Point StartPoint { get; }
        public Direction Direction1 { get; }

        public enum Direction
        {
            Horizontal, Vertical
        }

        public enum Result
        {
            Hit, Miss, Sunk
        }

        public Ship(Point startPoint, Direction direction)
        {
            StartPoint = startPoint;
            Direction1 = direction;
        }

        public Result UnderAttack(Point point)
        {

            return Result.Hit;
        }
    }
}
