using System;
using Gomoku.Core.Core;
using Gomoku.Core.Enums;

namespace Gomoku.AI
{
    public class Simpler
    {
        private int _depth = 5;
        private bool _iAmBlack = true;

        public Simpler(Colors color)
        {
            _iAmBlack = color == Colors.Black ? true : false;
        }

        public string GetLine(Board board, int x, int y, Direction direction)
        {

            string result = "";
            switch (direction)
            {
                case Direction.HORIZONTAL:
                    result = GetHor(board, x, y);
                    break;
                case Direction.VERTICAL:
                    result = GetVer(board, x, y);
                    break;
                case Direction.FIRST_DIAGONAL:
                    result = GetFDig(board, x, y);
                    break;
                case Direction.SECOND_DIAGONAL:
                    result = GetSDig(board, x, y);
                    break;
                default:
                    break;
            }
            if (_iAmBlack)
            {
                return result.Replace('B','X').Replace('W', 'Y');
            }
            return result.Replace('W', 'X').Replace('B', 'Y');
        }

        private string GetSDig(Board board, int x, int y)
        {
            string result = "*";
            bool leftBottomLock = false;
            bool topRightLock = false;

            for (int i = 1; i < _depth; i++)
            {
                if (!leftBottomLock && x - i >= 0 && y - i >= 0 )
                {
                    result = result.Insert(0, board.GameBoard[x - i, y - i].ToString());
                }
                else
                {
                    leftBottomLock = true;
                }

                if (!topRightLock && x + i < board.Size && y + i < board.Size)
                {
                    result += board.GameBoard[x + i, y + i].ToString();
                }
                else
                {
                    topRightLock = true;
                }
                if (leftBottomLock && topRightLock)
                {
                    return result;
                }
            }
            return result;
        }

        private string GetFDig(Board board, int x, int y)
        {
            string result = "*";
            bool leftTopLock = false;
            bool bottomRightLock = false;

            for (int i = 1; i < _depth; i++)
            {
                if (!leftTopLock && x - i >= 0 && y + i < board.Size)
                {
                     result = result.Insert(0, board.GameBoard[x - i, y + i].ToString());
                }
                else
                {
                    leftTopLock = true;
                }

                if (!bottomRightLock && x + i < board.Size && y - i >= 0)
                {
                    result += board.GameBoard[x + i, y - i].ToString();
                }
                else
                {
                    bottomRightLock = true;
                }
                if (leftTopLock && bottomRightLock)
                {
                    return result;
                }
            }
            return result;
        }

        private string GetVer(Board board, int x, int y)
        {
            string result = "*";

            bool topLock = false;
            bool bottomLock = false;

            for (int i = 1; i <= _depth; i++)
            {
                if (!topLock && y + i < board.Size)
                {
                    result = result.Insert(0, board.GameBoard[x, y + i].ToString());
                }
                else
                {
                    topLock = true;
                }

                if (!bottomLock && y - i >= 0)
                {
                    result += board.GameBoard[x, y - i].ToString();
                }
                else
                {
                    bottomLock = true;
                }
                if (topLock && bottomLock)
                {
                    return result;
                }
            }
            return result;
        }

        private string GetHor(Board board, int x, int y)
        {
            string result = "*";
            bool rightLock = false;
            bool leftLock = false;

            for (int i = 1; i < _depth; i++)
            {
                if (!rightLock && x + i < board.Size)
                {
                    result = result.Insert(0, board.GameBoard[x + i, y].ToString());
                }
                else
                {
                    rightLock = true;
                }

                if (!leftLock && x - i >= 0)
                {
                    result += board.GameBoard[x - i, y];
                }
                else
                {
                    leftLock = true;
                }
                if (rightLock && leftLock)
                {
                    return result;
                }
            }
            return result;
        }
    }
}
