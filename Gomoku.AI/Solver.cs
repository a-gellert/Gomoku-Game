using Gomoku.Core.Core;
using Gomoku.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gomoku.AI
{
    public class Solver
    {
        private static readonly Random random = new Random();

        private List<Point> _potentialPoints;
        private Dictionary<Point, int> _potMoves;
        private Elements el;
        private Board _board;
        private Seeker _simpler;
        private ExcludingPoints _exPoints;
        Dictionary<string, int> _patterns;

        private Dictionary<Point, int> _forks;

        public Solver(Dictionary<string, int> patterns, Colors color)
        {
            _exPoints = new ExcludingPoints();
            _potMoves = new Dictionary<Point, int>();
            _forks = new Dictionary<Point, int>();
            _potentialPoints = new List<Point>();
            _simpler = new Seeker(color);
            el = color == Colors.Black ? Elements.BLACK_STONE : Elements.WHITE_STONE;
            _patterns = patterns;
        }

        public Point GetMove(Board board)
        {
            _potMoves.Clear();
            _forks.Clear();
            _board = board;

            _potentialPoints = _exPoints.ExcludePoints(board, _potentialPoints);
            return MovesAnalize();
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

            if (sum < 2000 && _forks.Count > 0)
            {
                point = _forks.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            }
            else
            {
                var l = _potMoves.Where((x) => x.Value == sum).Select(x => x.Key).ToList();
                point = l[random.Next(0, l.Count)];
            }

            return point;
        }
        private int CheckCost(Point point)
        {
            int x = point.X;
            int y = point.Y;

            int cost = OnHorizontal(x, y) + OnVertical(x, y) + OnFirstDiagonal(x, y) + OnSecondDiagonal(x, y);
            if (cost % 10 != 0)
            {
                _forks.Add(point, cost);
            }
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


    }
}
