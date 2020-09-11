using Gomoku.Core.Core;

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
