using Gomoku.Core.Core;
using Gomoku.Core.Enums;
using System.Collections.Generic;

namespace Gomoku.AI
{
    public class Medium : Player
    {
        private Dictionary<string, int> _patterns = GamePatterns.MediumPatterns;
        private Colors _color;
        private Solver Solver;

        public Medium(Colors color)
        {
            Solver = new Solver(_patterns, color);
            _color = color;
        }

        public override Point MakeMove(Board board)
        {
            Point point = Solver.GetMove(board);
            return point;
        }
    }
}
