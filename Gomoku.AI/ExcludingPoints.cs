using Gomoku.Core.Core;
using Gomoku.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.AI
{
    public class ExcludingPoints
    {
        private Board _board;
        private List<Point> _potentialPoints;

        public List<Point> ExcludePoints(Board board, List<Point> points)
        {
            _board = board;
            _potentialPoints = points;

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < _board.Size; j++)
                {
                    HandlePoint(i, j);
                }
            }
            return _potentialPoints;
        }

        private void HandlePoint(int x, int y)
        {
            bool isNear = CheckNeighbours(x, y);

            if (ElType(x, y) == Elements.EMPTY_CELL && !_potentialPoints.Contains(new Point(x, y)) && isNear)
            {
                _potentialPoints.Add(new Point(x, y));
            }
            else if (!(ElType(x, y) == Elements.EMPTY_CELL) && _potentialPoints.Contains(new Point(x, y)))
            {
                _potentialPoints.Remove(new Point(x, y));
            }
            else if (x == 7 && y == 7)
            {
                _potentialPoints.Add(new Point(x, y));

            }
        }
        private bool CheckNeighbours(int x, int y)
        {
            char w = (char)Elements.WHITE_STONE;
            char b = (char)Elements.BLACK_STONE;

            if (x - 1 >= 0 && (_board.GameBoard[x - 1, y] == w || _board.GameBoard[x - 1, y] == b))
            {
                return true;
            }
            if (x + 1 < _board.Size && (_board.GameBoard[x + 1, y] == w || _board.GameBoard[x + 1, y] == b))
            {
                return true;
            }
            if (y - 1 >= 0 && (_board.GameBoard[x, y - 1] == w || _board.GameBoard[x, y - 1] == b))
            {
                return true;

            }
            if (y + 1 < _board.Size && (_board.GameBoard[x, y + 1] == w || _board.GameBoard[x, y + 1] == b))
            {
                return true;
            }
            if (x - 1 >= 0 && y - 1 >= 0 && (_board.GameBoard[x - 1, y - 1] == w || _board.GameBoard[x - 1, y - 1] == b))
            {
                return true;
            }
            if (x + 1 < _board.Size && y + 1 < _board.Size && (_board.GameBoard[x + 1, y + 1] == w || _board.GameBoard[x + 1, y + 1] == b))
            {
                return true;
            }
            if (x + 1 < _board.Size && y - 1 >= 0 && (_board.GameBoard[x + 1, y - 1] == w || _board.GameBoard[x + 1, y - 1] == b))
            {
                return true;

            }
            if (x - 1 >= 0 && y + 1 < _board.Size && (_board.GameBoard[x - 1, y + 1] == w || _board.GameBoard[x - 1, y + 1] == b))
            {
                return true;
            }
            return false;
        }
        private Elements ElType(int x, int y)
        {
            if (x > _board.Size || y > _board.Size)
            {
                return Elements.NONE;
            }
            if (_board.GameBoard[x, y] == (char)Elements.BLACK_STONE)
            {
                return Elements.BLACK_STONE;
            }
            if (_board.GameBoard[x, y] == (char)Elements.WHITE_STONE)
            {
                return Elements.WHITE_STONE;
            }
            return Elements.EMPTY_CELL;
        }
    }
}
