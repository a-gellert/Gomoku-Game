namespace Gomoku.Console
{
    using Gomoku.AI;
    using Gomoku.Core.Core;
    using Gomoku.Core.Enums;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            GomokuGame game = new GomokuGame();

            Solver solver1 = new Solver();
            Solver solver2 = new Solver();

            DateTime turnTime = DateTime.UtcNow;
            TimeSpan timer;
            GameStates run = GameStates.GAME_IS_RUNNING;


            while (game.State == run)
            {
                Point move = new Point(0, 0);

                while (game.State == run)
                {
                    timer = DateTime.UtcNow - turnTime;
                    move = solver1.MakeMove();
                    if (game.MakeMove(move))
                    {
                        turnTime = DateTime.UtcNow;
                        break;
                    }
                    else if (timer.Seconds >= 2)
                    {
                        game.TurnTimeIsOver();

                        return;
                    }
                }
                while (game.State == run)
                {
                    timer = DateTime.UtcNow - turnTime;
                    move = solver2.MakeMove();
                    if (game.MakeMove(move))
                    {
                        turnTime = DateTime.UtcNow;
                        break;
                    }
                    else if (timer.Seconds >= 2)
                    {
                        game.TurnTimeIsOver();

                        return;
                    }
                }


                Console.SetCursorPosition(0, 0);
                Console.WriteLine(game.BoardAsString());
            }
            Console.WriteLine(game.State.ToString());
            game.Board.wonPoints.ForEach(x => Console.WriteLine(x));

            Console.ReadLine();
        }
      
    }
}
