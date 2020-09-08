using Gomoku.Core.Core;
using System;

namespace Gomoku.AI
{
    public class Solver
    {
        private static readonly Random random = new Random();
        private int _x;
        private int _y;
        public Point MakeMove()
        {
            _x = random.Next(0, 15);
            _y = random.Next(0, 15);

            return new Point(_x, _y);
        }
    }
}
