using Gomoku.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.AI
{
    public abstract class Player
    {

        public virtual Point MakeMove(Board board)
        {
            return new Point(0,0);
        }

    }
}
