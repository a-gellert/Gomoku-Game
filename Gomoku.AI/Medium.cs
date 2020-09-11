using Gomoku.Core.Core;
using Gomoku.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gomoku.AI
{
    public class Solver : Player
    {
        private static readonly Random random = new Random();

        private Dictionary<string, int> _patterns = GamePatterns.Patterns;
        private List<Point> _potentialPoints;
        private Dictionary<Point, int> _potMoves;
        private Elements el;
        private int _move = 1;
        private Board _board;
        private Simpler _simpler;

        public Solver(Colors color)
        {
            _potMoves = new Dictionary<Point, int>();
            _potentialPoints = new List<Point>();
            _simpler = new Simpler(color);
            el = color == Colors.Black ? Elements.BLACK_STONE : Elements.WHITE_STONE;
            // _move = color == Colors.Black ? 1 : 2;
        }

        public override Point MakeMove(Board board)
        {

            _potMoves.Clear();
            _board = board;

            ExcludePoints();
            Point point = MovesAnalize();

            return point;

        }

        private Point MovesAnalize()
        {
            int sum = 0;
            Point point = new Point(0, 0);

            _potentialPoints.ForEach(p =>
            {
                _potMoves.Add(p, CheckCost(p));
            });

            sum = _potMoves.Aggregate((x, y) => x.Value > y.Value ? x : y).Value;

            var l = _potMoves.Where((x) => x.Value == sum).Select(x => x.Key).ToList();
            point = l[random.Next(0, l.Count)];

            return point;
        }
        private int CheckCost(Point point)
        {
            int x = point.X;
            int y = point.Y;

            int cost = OnHorizontal(x, y) + OnVertical(x, y) + OnFirstDiagonal(x, y) + OnSecondDiagonal(x, y);

            return cost;
        }

        private int OnHorizontal(int x, int y)
        {
            string str = _simpler.GetLine(_board, x, y, Direction.HORIZONTAL);

            return CheckPattern(str);
        }
        private int OnVertical(int x, int y)
        {
            string str = _simpler.GetLine(_board, x, y, Direction.VERTICAL);

            return CheckPattern(str);
        }
        private int OnFirstDiagonal(int x, int y)
        {
            string str = _simpler.GetLine(_board, x, y, Direction.FIRST_DIAGONAL);

            return CheckPattern(str);
        }
        private int OnSecondDiagonal(int x, int y)
        {
            string str = _simpler.GetLine(_board, x, y, Direction.SECOND_DIAGONAL);

            return CheckPattern(str);
        }

        private int CheckPattern(string str)
        {
            string reverse = new string(str.Reverse().ToArray());

            foreach (var item in _patterns.Keys)
            {
                if (str.Contains(item.ToString()))
                {
                    return _patterns[item];
                }
                else if (reverse.Contains(item.ToString()))
                {
                    return _patterns[item];
                }
            }

            return 0;
        }

        private void ExcludePoints()
        {
            Fill(15);
            return;
            switch (_move)
            {
                case 1:
                    Fill(3);
                    break;
                case 2:
                    Fill(5);
                    break;
                case 3:
                    Fill(7);
                    break;
                case 4:
                    Fill(9);
                    break;
                default:
                    Fill(15);
                    break;
            }
        }

        private void Fill(int counter)
        {
            for (int i = 0; i < counter; i++)
            {
                for (int j = 0; j < counter; j++)
                {
                    HandlePoint(i, j);
                }
            }
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
            else if (_move == 1 && x==7 && y==7)
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
