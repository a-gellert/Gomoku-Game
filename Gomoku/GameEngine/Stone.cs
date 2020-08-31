using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.GameEngine
{
    public class Stone
    {
        public Point Point { get; private set; }

        public Stone(Point point)
        {
            Point = point;
        }
    }
}
