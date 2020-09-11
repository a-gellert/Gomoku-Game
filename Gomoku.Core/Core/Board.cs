using Gomoku.Core.Enums;
using System.Collections.Generic;

namespace Gomoku.Core.Core
{
    public class Board
    {
        private readonly int WinnigStonesCount = 5;
        public int Size { get; private set; }
        public char[,] GameBoard { get; private set; }

        private char _emptyCell = (char)Elements.EMPTY_CELL;
        private char _blackStone = (char)Elements.BLACK_STONE;
        private char _whiteStone = (char)Elements.WHITE_STONE;

        public List<Point> WonPoints { get; private set; }

        public Board(int size)
        {
            Size = size;
            GameBoard = InitialFill();
            WonPoints = new List<Point>();
        }

        private char[,] InitialFill()
        {
            char[,] emptyBoard = new char[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    emptyBoard[i, j] = (char)Elements.EMPTY_CELL;
                }
            }
            return emptyBoard;
        }

        public bool HasFive(Point point)
        {
            int x = point.X;
            int y = point.Y;

            if (HorizontalCheck(x, y) || VerticalCheck(x, y) || FirstDiagonalCheck(x, y) || SecondDiagonalCheck(x, y))
            {
                return true;
            }
            return false;
        }

        public bool MayPutOn(Point point)
        {
            int x = point.X;
            int y = point.Y;

            if (x > Size || y > Size || x < 0 || y < 0)
            {
                return false;
            }
            if (!(GameBoard[x, y] == _emptyCell))
            {
                return false;
            }
            return true;
        }

        public void Add(Point point, Colors color)
        {
            int x = point.X;
            int y = point.Y;
            if (color == Colors.Black)
            {
                GameBoard[x, y] = _blackStone;
                return;
            }
            GameBoard[x, y] = _whiteStone;
        }

        private bool HorizontalCheck(int x, int y)
        {
            WonPoints.Clear();
            WonPoints.Add(new Point(x , y ));

            int count = 1;
            bool rightLock = false;
            bool leftLock = false;

            for (int i = 1; i < WinnigStonesCount; i++)
            {
                if (!rightLock && x + i < Size && GameBoard[x + i, y] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x + i, y ));
                }
                else
                {
                    rightLock = true;
                }

                if (!leftLock && x - i >= 0 && GameBoard[x - i, y] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x - i, y ));
                }
                else
                {
                    leftLock = true;
                }
                if (count == 5)
                {
                    return true;
                }
                else if (leftLock == true && rightLock == true)
                {
                    return false;
                }
            }
            return false;
        }
        private bool VerticalCheck(int x, int y)
        {
            WonPoints.Clear();
            WonPoints.Add(new Point(x , y ));

            int count = 1;
            bool topLock = false;
            bool bottomLock = false;

            for (int i = 1; i <= WinnigStonesCount; i++)
            {
                if (!topLock && y + i < Size && GameBoard[x, y + i] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x , y  + i));
                }
                else
                {
                    topLock = true;
                }

                if (!bottomLock && y - i >= 0 && GameBoard[x, y - i] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x , y  - i));
                }
                else
                {
                    bottomLock = true;
                }
                if (count == 5)
                {
                    return true;
                }
                if (topLock == true && bottomLock == true)
                {
                    return false;
                }
            }
            return false;
        }
        private bool FirstDiagonalCheck(int x, int y)
        {
            WonPoints.Clear();
            WonPoints.Add(new Point(x , y ));

            int count = 1;

            bool leftTopLock = false;
            bool bottomRightLock = false;

            for (int i = 1; i < WinnigStonesCount; i++)
            {
                if (!leftTopLock && x - i >= 0 && y + i < Size && GameBoard[x - i, y + i] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x  - i, y  + i));
                }
                else
                {
                    leftTopLock = true;
                }

                if (!bottomRightLock && x + i < Size && y - i >= 0 && GameBoard[x + i, y - i] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x  + i, y  - i));
                }
                else
                {
                    bottomRightLock = true;
                }
                if (count == 5)
                {
                    return true;
                }
                if (leftTopLock == true && bottomRightLock == true)
                {
                    return false;
                }
            }

            return false;
        }
        private bool SecondDiagonalCheck(int x, int y)
        {
            WonPoints.Clear();
            WonPoints.Add(new Point(x , y ));

            int count = 1;

            bool leftBottomLock = false;
            bool topRightLock = false;

            for (int i = 1; i < WinnigStonesCount; i++)
            {
                if (!leftBottomLock && x - i >= 0 && y - i >= 0 && GameBoard[x - i, y - i] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x  - i, y  - i));
                }
                else
                {
                    leftBottomLock = true;
                }

                if (!topRightLock && x + i < Size && y + i < Size && GameBoard[x + i, y + i] == GameBoard[x, y])
                {
                    count++;
                    WonPoints.Add(new Point(x  + i, y  + i));
                }
                else
                {
                    topRightLock = true;
                }
                if (count == 5)
                {
                    return true;
                }
                if (leftBottomLock == true && topRightLock == true)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
