namespace Gomoku.Console
{
    using Gomoku.AI;
    using Gomoku.Core.Core;
    using Gomoku.Core.Enums;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            GomokuGame game = new GomokuGame();


            Medium solver1 = new Medium(Colors.Black);
            Medium solver2 = new Medium(Colors.White);

            DateTime turnTime = DateTime.UtcNow;
            TimeSpan timer;
            GameStates run = GameStates.GAME_IS_RUNNING;

            while (game.State == run)
            {
                Point move = new Point(0, 0);

                while (game.State == run)
                {
                    timer = DateTime.UtcNow - turnTime;
                    move = solver1.MakeMove(game.Board);
                    if (game.MakeMove(move))
                    {
                        turnTime = DateTime.UtcNow;
                        break;
                    }
                    //else if (timer.Seconds >= 2)
                    //{
                    //    game.TurnTimeIsOver();

                    //    return;
                    //}
                }
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(game.BoardAsString());
                while (game.State == run)
                {
                    timer = DateTime.UtcNow - turnTime;
                    move = solver2.MakeMove(game.Board);
                  // move = ToMove(Console.ReadLine());
                    if (game.MakeMove(move))
                    {
                        turnTime = DateTime.UtcNow;
                        break;
                    }
                    //else if (timer.Seconds >= 2)
                    //{
                    //    game.TurnTimeIsOver();

                    //    return;
                    //}
                }
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(game.BoardAsString());

            }
            Console.WriteLine(game.State.ToString());
            game.Board.WonPoints.ForEach(x => Console.WriteLine(x));

            Console.ReadLine();
        }

        private static Point ToMove(string v)
        {
            int[] inp = v.Split(' ').Select(int.Parse).ToArray();
            return new Point(inp[0]-1, inp[1]-1);
        }
    }
}
