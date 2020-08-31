using Gomoku.GameEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.GameEngine
{
    public class GomokuGame
    {   //Constants
        private readonly int BoardSize = 15;
        private readonly int MaxMovesCounter = 225;

        public int MovesCounter { get; set; }
        public List<Stone> Stones { get; set; }

        public GomokuGame()
        {
            MovesCounter = 1;

        }

        public GameStates GetState()
        {
            bool hasFive = HasFive();
            bool blackTurn =  MovesCounter % 2 == 1;

            if (hasFive && blackTurn)
            {
                return GameStates.BLACK_WON;
            }
            if (hasFive && !blackTurn)
            {
                return GameStates.WHITE_WON;
            }
            if (MovesCounter == MaxMovesCounter)
            {
                return GameStates.WHITE_WON;
            }
            return GameStates.GAME_IS_RUNNING;
        }

        public bool HasFive()
        {
            return false;
        }
    }
}
