using Gomoku.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.AI
{
    public class Human : Player
    {
        public Point Point { get; private set; }
        public override Point MakeMove(Board board)
        {
            return Point;
        }

        public void SetPoint(int x, int y)
        {
            Point = new Point(x, y);
        }
    }
}
