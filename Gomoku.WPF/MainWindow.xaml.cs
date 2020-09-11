using Gomoku.AI;
using Gomoku.Core.Core;
using Gomoku.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Point = Gomoku.Core.Core.Point;

namespace Gomoku.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double Offset = 34.286;
        private readonly int Border = 44;

        private DispatcherTimer timer = null;


        bool _isBlack;
        private GomokuGame _game;

        GameStates run = GameStates.GAME_IS_RUNNING;

        private Solver solverBlack;
        private Solver solverWhite;

        private Human humanBlack;
        private Human humanWhite;

        private Player blackPlayer;
        private Player whitePlayer;
        private int _x;
        private int _y;
        private bool isMoveDone;

        public MainWindow()
        {
            _game = new GomokuGame();
            _isBlack = true;
            InitializeComponent();
        }
        private void timerStart()
        {
            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (_isBlack)
            {
                while (_game.State == run)
                {
                    Point move = radCompLeft.IsChecked == true ? new Point(0, 0) : SetPoint() ;

                    move = blackPlayer.MakeMove(_game.Board);
                    if (_game.MakeMove(move))
                    {
                        Image black = new Image();
                        SetImage(black, move.X, move.Y);
                        moves.Text += "b" + move.ToString();
                        _isBlack = !_isBlack;
                        break;
                    }
                }
            }
            else
            {
                while (_game.State == run)
                {
                    Point move = radCompRight.IsChecked == true ? new Point(0, 0) : SetPoint();

                    move = whitePlayer.MakeMove(_game.Board);
                    // move = ToMove(Console.ReadLine());
                    if (_game.MakeMove(move))
                    {
                        Image white = new Image();
                        SetImage(white, move.X, move.Y);
                        moves.Text += "w" + move.ToString() + "\n";
                        _isBlack = !_isBlack;
                        break;
                    }

                }
            }
            if (_game.State == GameStates.BLACK_WON)
            {
                string msg = "Black player won!";
                ShowWinner(msg);

            }
            else if (_game.State == GameStates.WHITE_WON)
            {
                string msg = "White player won!";
                ShowWinner(msg);
            }
            else if (_game.State == GameStates.DRAW)
            {
                string msg = "Draw!";
                ShowWinner(msg);
            }
        }

        private Point SetPoint()
        {
            throw new NotImplementedException();
        }

        private void ShowWinner(string msg)
        {

            timer.Stop();
            List<Point> points = _game.Board.WonPoints;

            Point start = points[0];
            Point end = points[0];

            for (int i = 0; i < points.Count; i++)
            {
                if (Point.Compare(start, points[i]))
                {
                    start = points[i];
                }
                if (!Point.Compare(end, points[i]))
                {
                    end = points[i];
                }
            }
            Line winLine = new Line();

            winLine.StrokeThickness = 3;
            winLine.Stroke = Brushes.Tomato;
            winLine.X1 = start.X * Offset + Border;
            winLine.Y1 = start.Y * Offset + Border;
            winLine.X2 = end.X * Offset + Border;
            winLine.Y2 = end.Y * Offset + Border;

            canvas.Children.Add(winLine);
            MessageBox.Show(msg);
            _game.Restart();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetPlayers();
            Restart();

            timerStart();

        }

        private void Restart()
        {
            _isBlack = true;
            canvas.Children.Clear();
            _game.Restart();
            timer?.Stop();
        }

        private void SetPlayers()
        {
            if (radCompLeft.IsChecked == true && radCompRight.IsChecked == true)
            {
                solverBlack = new Solver(Core.Enums.Colors.Black);
                solverWhite = new Solver(Core.Enums.Colors.White);

                blackPlayer = solverBlack;
                whitePlayer = solverWhite;
            }
            else if (radCompLeft.IsChecked == true && radHumanRight.IsChecked == true)
            {
                solverBlack = new Solver(Core.Enums.Colors.Black);
                humanWhite = new Human();

                blackPlayer = solverBlack;
                whitePlayer = humanWhite;
            }
            else if (radHumanLeft.IsChecked == true && radCompRight.IsChecked == true)
            {
                humanBlack = new Human();
                solverWhite = new Solver(Core.Enums.Colors.White);

                blackPlayer = humanBlack;
                whitePlayer = solverWhite;

            }
            else if (radHumanLeft.IsChecked == true && radHumanRight.IsChecked == true)
            {
                humanBlack = new Human();
                humanWhite = new Human();

                blackPlayer = humanBlack;
                whitePlayer = humanWhite;

            }
        }
        private void SetImage(Image image, int x, int y)
        {
            if (x > 14 || x < 0 || y > 14 || y < 0)
            {
                return;
            }
            string imgSource = _isBlack ? @"C:\Users\Admin\source\repos\Gomoku\Gomoku.WPF\stuff\circleBlack.png" : @"C:\Users\Admin\source\repos\Gomoku\Gomoku.WPF\stuff\circleWhite.png";
            image.Width = 32;
            image.Source = new BitmapImage(new Uri(imgSource));
            Canvas.SetLeft(image, x * Offset + Border);
            Canvas.SetTop(image, y * Offset + Border);
            canvas.Children.Add(image);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            _x = GetCell(e.GetPosition(canvas).X);
            _y = GetCell(e.GetPosition(canvas).Y);

            Image image = new Image();

            if (radHumanLeft.IsChecked == true)
            {
                humanBlack.SetPoint(_x, _y);
            }
            else if (radHumanRight.IsChecked == true)
            {
                humanWhite.SetPoint(_x, _y);
            }
            SetImage(image, _x, _y);

            moves.Text = _x.ToString() + " " + _y.ToString();
        }

        private int GetCell(double coord)
        {
            return (int)((coord - Border) / Offset);
        }
    }
}
