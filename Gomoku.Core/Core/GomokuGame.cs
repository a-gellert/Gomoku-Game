using Gomoku.Core.Enums;
using System;

namespace Gomoku.Core.Core
{
    public class GomokuGame
    {   //Constants
        public readonly int BoardSize = 15;
        private readonly int MaxMovesCounter = 225;

        private static Random _rnd = new Random();
        private bool _hasFive = false;

        public int MovesCounter { get; private set; }
        public Board Board { get; private set; }
        public GameStates State { get; private set; }
        private bool _turnTimeIsOver;

        public GomokuGame()
        {
            Board = new Board(BoardSize);
            MovesCounter = 1;
            _turnTimeIsOver = false;
            GetState();
        }

        public bool MakeMove(Point point)
        {
            if (MovesCounter == 1)
            {
                Board.Add(new Point(7,7), Colors.Black);
                MovesCounter++;
                return true;
            }
            bool mayPutOn = MayPutOn(point);

            if (mayPutOn)
            {
                Colors color = MovesCounter % 2 == 1 ? Colors.Black : Colors.White;

                Board.Add(point, color);

                _hasFive = Board.HasFive(point);

                GetState();
                if (_hasFive) return true;

                MovesCounter++;
                return mayPutOn;
            }
            return mayPutOn;
        }

        private void GetState()
        {
            bool blackTurn = MovesCounter % 2 == 1;

            if ((_turnTimeIsOver || _hasFive) && blackTurn)
            {
                State = GameStates.BLACK_WON;
                return;
            }
            if ((_turnTimeIsOver || _hasFive) && !blackTurn)
            {
                State = GameStates.WHITE_WON;
                return;
            }
            if (MovesCounter == MaxMovesCounter)
            {
                State = GameStates.DRAW;
                return;
            }
            State = GameStates.GAME_IS_RUNNING;
        }

        public bool MayPutOn(Point point)
        {
            GetState();
            return Board.MayPutOn(point);
        }

        public string BoardAsString()
        {
            string str = "";
            for (int i = BoardSize - 1; i >= 0; i--)
            {
                str += i + 1+"\t";
                for (int j = 0; j < BoardSize; j++)
                {
                    str += Board.GameBoard[j, i] + " ";
                }
                str += "\n";
            }
            return str;
        }

        public void TurnTimeIsOver()
        {
            _turnTimeIsOver = true;
            MovesCounter++;
            GetState();
        }

    }
}
