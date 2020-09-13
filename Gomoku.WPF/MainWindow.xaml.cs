using Gomoku.AI;
using Gomoku.Core.Core;
using Gomoku.Core.Enums;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private Medium mediumBlack;
        private Medium mediumWhite;

        private Easy easyBlack;
        private Easy easyWhite;

        private Human humanBlack;
        private Human humanWhite;

        private Player blackPlayer;
        private Player whitePlayer;
        private int _x;
        private int _y;

        private bool _firstIsBot = true;
        private bool _secondIsBot = true;
        private Point _point;

        Ellipse ellipseAim;
        Ellipse ellipseMove;

        public MainWindow()
        {
            _game = new GomokuGame();
            _isBlack = true;
            InitializeComponent();
            ellipseMove = new Ellipse();
            ellipseAim = new Ellipse();
            window.Background = Brushes.Gray;
        }
        private void timerStart()
        {
            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (_isBlack && !_firstIsBot)
            {
                timer.Stop();
                return;
            }
            else if (!_isBlack && !_secondIsBot)
            {
                timer.Stop();
                return;
            }

            DoMove();

        }

        private void DoMove()
        {
            Player player = _isBlack ? blackPlayer : whitePlayer;
            string liter = _isBlack ? "\nB" : "W";

            while (_game.State == run)
            {
                Point move = _point;

                move = player.MakeMove(_game.Board);
                if (_game.MakeMove(move))
                {
                    Image black = new Image();
                    SetStoneImage(black, move.X, move.Y);
                    moves.Text += $"{liter} ({move.X + 1}; {15 - move.Y}) ";
                    _isBlack = !_isBlack;
                    break;
                }
                return;
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

        private void SetPoint(int x, int y)
        {
            if (_game.MovesCounter == 1 && !_firstIsBot)
            {
                timerStart();
                humanBlack.SetPoint(7, 7);
            }
            else if (_isBlack && !_firstIsBot)
            {
                timerStart();
                humanBlack.SetPoint(x, y);
            }
            else if (_isBlack && _firstIsBot)
            {
                _point = new Point(0, 0);
            }
            else if (!_isBlack && !_secondIsBot)
            {
                timerStart();
                humanWhite.SetPoint(x, y);
            }
            else if (!_isBlack && _secondIsBot)
            {
                _point = new Point(0, 0);
            }

            timer.Start();
        }

        private void ShowWinner(string msg)
        {

            timer.Stop();
            List<Point> points = _game.Board.WonPoints;
            points.ForEach(p =>
            {
                Ellipse ellipseWin = new Ellipse();

                ellipseWin.Width = 16;
                ellipseWin.Height = 16;

                Canvas.SetLeft(ellipseWin, p.X * Offset + Border + 8);
                Canvas.SetTop(ellipseWin, p.Y * Offset + Border + 8);


                RadialGradientBrush myBrush = new RadialGradientBrush();

                myBrush.GradientStops.Add(new GradientStop(System.Windows.Media.Colors.Red, 0.0));
                if (!_isBlack)
                {
                    myBrush.GradientStops.Add(new GradientStop(System.Windows.Media.Colors.White, 1.0));
                }
                else
                {
                    myBrush.GradientStops.Add(new GradientStop(System.Windows.Media.Colors.Black, 1.0));
                }

                ellipseWin.Fill = myBrush;

                canvas.Children.Add(ellipseWin);
            });


            MessageBox.Show(msg);
            window.Background = Brushes.Gray;

            canvas.IsManipulationEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            canvas.IsManipulationEnabled = true;
            window.Background = Brushes.White;

            moves.Text = "";
            _isBlack = true;
            canvas.Children.Clear();
            _game.Restart();
            timer?.Stop();

            SetPlayers();
            if (_firstIsBot)
            {
                timerStart();
            }
        }

        private void SetPlayers()
        {
            if (radMediumLeft.IsChecked == true && radMediumRight.IsChecked == true)
            {
                mediumBlack = new Medium(Core.Enums.Colors.Black);
                mediumWhite = new Medium(Core.Enums.Colors.White);

                blackPlayer = mediumBlack;
                whitePlayer = mediumWhite;

                _firstIsBot = true;
                _secondIsBot = true;
            }
            else if (radMediumLeft.IsChecked == true && radEasyRight.IsChecked == true)
            {
                easyBlack = new Easy(Core.Enums.Colors.Black);
                easyWhite = new Easy(Core.Enums.Colors.White);

                blackPlayer = easyBlack;
                whitePlayer = easyWhite;

                _firstIsBot = true;
                _secondIsBot = true;
            }
            else if (radMediumLeft.IsChecked == true && radHumanRight.IsChecked == true)
            {
                mediumBlack = new Medium(Core.Enums.Colors.Black);
                humanWhite = new Human();

                blackPlayer = mediumBlack;
                whitePlayer = humanWhite;

                _firstIsBot = true;
                _secondIsBot = false;
            }

            else if (radEasyLeft.IsChecked == true && radEasyRight.IsChecked == true)
            {
                easyBlack = new Easy(Core.Enums.Colors.Black);
                easyWhite = new Easy(Core.Enums.Colors.White);

                blackPlayer = easyBlack;
                whitePlayer = easyWhite;

                _firstIsBot = true;
                _secondIsBot = true;
            }
            else if (radEasyLeft.IsChecked == true && radMediumRight.IsChecked == true)
            {
                easyBlack = new Easy(Core.Enums.Colors.Black);
                mediumWhite = new Medium(Core.Enums.Colors.White);

                blackPlayer = easyBlack;
                whitePlayer = mediumWhite;

                _firstIsBot = true;
                _secondIsBot = true;
            }
            else if (radEasyLeft.IsChecked == true && radHumanRight.IsChecked == true)
            {
                easyBlack = new Easy(Core.Enums.Colors.Black);
                humanWhite = new Human();

                blackPlayer = easyBlack;
                whitePlayer = humanWhite;

                _firstIsBot = true;
                _secondIsBot = false;
            }

            else if (radHumanLeft.IsChecked == true && radMediumRight.IsChecked == true)
            {
                humanBlack = new Human();
                mediumWhite = new Medium(Core.Enums.Colors.White);

                blackPlayer = humanBlack;
                whitePlayer = mediumWhite;

                _firstIsBot = false;
                _secondIsBot = true;
            }
            else if (radHumanLeft.IsChecked == true && radHumanRight.IsChecked == true)
            {
                humanBlack = new Human();
                humanWhite = new Human();

                blackPlayer = humanBlack;
                whitePlayer = humanWhite;

                _firstIsBot = false;
                _secondIsBot = false;
            }
            else if (radHumanLeft.IsChecked == true && radEasyRight.IsChecked == true)
            {
                humanBlack = new Human();
                easyWhite = new Easy(Core.Enums.Colors.White);

                blackPlayer = humanBlack;
                whitePlayer = easyWhite;

                _firstIsBot = false;
                _secondIsBot = true;
            }
        }
        private void SetStoneImage(Image image, int x, int y)
        {
            if (x > 14 || x < 0 || y > 14 || y < 0)
            {
                return;
            }

            if (canvas.Children.Contains(ellipseMove))
            {
                canvas.Children.Remove(ellipseMove);
            }

            ellipseMove.Width = 12;
            ellipseMove.Height = 12;
            ellipseMove.StrokeThickness = 3;
            ellipseMove.Stroke = Brushes.IndianRed;
            ellipseMove.Fill = Brushes.Black;
            Canvas.SetLeft(ellipseMove, x * Offset + Border+10);
            Canvas.SetTop(ellipseMove, y * Offset + Border+10);

            string imgSource = _isBlack ? @"C:\Users\Admin\source\repos\Gomoku\Gomoku.WPF\stuff\circleBlack.png" : @"C:\Users\Admin\source\repos\Gomoku\Gomoku.WPF\stuff\circleWhite.png";
            image.Width = 32;
            image.Source = new BitmapImage(new Uri(imgSource));
            Canvas.SetLeft(image, x * Offset + Border);
            Canvas.SetTop(image, y * Offset + Border);
            canvas.Children.Add(image);
            canvas.Children.Add(ellipseMove);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_firstIsBot && _secondIsBot)
            {
                return;
            }
            _x = GetCell(e.GetPosition(canvas).X);
            _y = GetCell(e.GetPosition(canvas).Y);

            SetPoint(_x, _y);

            DoMove();

            //timer.Start();
        }

        private int GetCell(double coord)
        {
            return (int)((coord - Border) / Offset);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            int x = GetCell(e.GetPosition(canvas).X);
            int y = GetCell(e.GetPosition(canvas).Y);

            if (canvas.Children.Contains(ellipseAim))
            {
                canvas.Children.Remove(ellipseAim);
            }

            ellipseAim.Width = 32;
            ellipseAim.Height = 32;

            Canvas.SetLeft(ellipseAim, x * Offset + Border);
            Canvas.SetTop(ellipseAim, y * Offset + Border);

            if (x > 14 || x < 0 || y > 14 || y < 0)
            {
                return;
            }

            if (_game.Board.GameBoard[x, y] == '+')
            {
                ellipseAim.Fill = Brushes.Cyan;
            }
            else
            {
                ellipseAim.Fill = Brushes.Tomato;
            }
            ellipseAim.Opacity = 0.5;
            canvas.Children.Add(ellipseAim);
        }
    }
}
