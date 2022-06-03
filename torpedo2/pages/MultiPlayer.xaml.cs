using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using torpedo2.data;
using torpedo2.json;

namespace Torpedo.pages
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Page
    {
        private bool gameOver;
        private const int GameSize = 10;
        private MainBoard gameBoard;
        private double unitX;
        private double unitY;

        public MultiPlayer()
        {
            InitializeComponent();
            InitGame();
            unitX = LeftCanvas.Width / GameSize;
            unitY = LeftCanvas.Height / GameSize;
        }

        private void RestartButttonClick(object sender, RoutedEventArgs e)
        {
            InitGame();
        }

        private void InitGame()
        {
            GameBoardGrid.Visibility = Visibility.Visible;
            NextPlayerGrid.Visibility = Visibility.Collapsed;
            GameOverGrid.Visibility = Visibility.Collapsed;
            gameBoard = new MainBoard("Player1", true, "Player2", true);
            gameOver = false;
            GameState();
        }

        private void GameState()
        {
            LeftCanvas.Children.Clear();
            RightCanvas.Children.Clear();
            if (gameBoard.TurnStarts)
            {
                for (int x = 0; x < GameSize; x++)
                {
                    for (int y = 0; y < GameSize; y++)
                    {
                        DrawPlayerPosition(x, y, gameBoard.GameMap1.Positions[x, y], LeftCanvas);
                        DrawEnemyPosition(x, y, gameBoard.GameMap2.Positions[x, y], RightCanvas);
                    }
                }
                PlayerNameLabel.Content = gameBoard.Player1.Name;
                RemainingEnemyShipsLabel.Content = gameBoard.Player2.Ships.RemainingShips().ToString();
                EnemyShipsHitLabel.Content = gameBoard.Player1.Hits.ToString();
                FiredTorpedosLabel.Content = gameBoard.Player1.Fires.ToString();
                RoundLabel.Content = gameBoard.Turn.ToString();
            }
            else
            {
                for (int x = 0; x < GameSize; x++)
                {
                    for (int y = 0; y < GameSize; y++)
                    {
                        DrawPlayerPosition(x, y, gameBoard.GameMap2.Positions[x, y], RightCanvas);
                        DrawEnemyPosition(x, y, gameBoard.GameMap1.Positions[x, y], LeftCanvas);
                    }
                }
                PlayerNameLabel.Content = gameBoard.Player2.Name;
                RemainingEnemyShipsLabel.Content = gameBoard.Player1.Ships.RemainingShips().ToString();
                EnemyShipsHitLabel.Content = gameBoard.Player2.Hits.ToString();
                FiredTorpedosLabel.Content = gameBoard.Player2.Fires.ToString();
                RoundLabel.Content = gameBoard.Turn.ToString();
            }
        }

        private void DrawPlayerPosition(int x, int y, Position position, Canvas canvas)
        {
            var shape = new Rectangle();

            //no ship, no hit in position
            if (position.Ship == 0 && !position.Hit)
            {
                shape.Fill = Brushes.LightBlue;
            }
            //no ship, position was hit
            else if (position.Ship == 0 && position.Hit)
            {
                shape.Fill = Brushes.Yellow;
            }
            //ship in position, not hit
            else if (position.Ship != 0 && !position.Hit)
            {
                shape.Fill = Brushes.DarkGray;
            }
            //ship in position, it was hit
            else if (position.Ship != 0 && position.Hit)
            {
                shape.Fill = Brushes.Red;
            }

            var unitX = canvas.Width / GameSize;
            var unitY = canvas.Height / GameSize;
            shape.Width = unitX;
            shape.Height = unitY;
            Canvas.SetTop(shape, y * unitY);
            Canvas.SetLeft(shape, x * unitX);
            canvas.Children.Add(shape);
        }

        private void DrawEnemyPosition(int x, int y, Position position, Canvas canvas)
        {
            var shape = new Rectangle();

            //no ship, no hit in position
            if (position.Ship == 0 && !position.Hit)
            {
                shape.Fill = Brushes.LightBlue;
            }
            //no ship, position was hit
            else if (position.Ship == 0 && position.Hit)
            {
                shape.Fill = Brushes.Yellow;
            }
            //ship in position, not hit
            else if (position.Ship != 0 && !position.Hit)
            {
                shape.Fill = Brushes.LightBlue;
            }
            //ship in position, it was hit
            else if (position.Ship != 0 && position.Hit)
            {
                shape.Fill = Brushes.Red;
            }

            var unitX = canvas.Width / GameSize;
            var unitY = canvas.Height / GameSize;
            shape.Width = unitX;
            shape.Height = unitY;
            Canvas.SetTop(shape, y * unitY);
            Canvas.SetLeft(shape, x * unitX);
            canvas.Children.Add(shape);

        }

        private void RightCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = Mouse.GetPosition(RightCanvas);
            if (!gameOver && gameBoard.TurnStarts)
            {
                bool player1Attacked = true;
                Position position = GetAttackedPosition(point, player1Attacked);
                var alreadyHit = position.Hit;
                if (!alreadyHit)
                {
                    gameBoard.LaunchTorpedo(position, gameBoard.Player1, gameBoard.Player2);
                    GameState();
                    if (gameBoard.Player2.Ships.RemainingShips() == 0)
                    {
                        bool player1Won = true;
                        GameOver(player1Won);
                    }
                    else
                    {
                        GoToNextPlayer();
                    }
                }
            }
        }

        private void LeftCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = Mouse.GetPosition(LeftCanvas);
            if (!gameOver && !gameBoard.TurnStarts)
            {

                bool player1Attacked = false;
                Position position = GetAttackedPosition(point, player1Attacked);
                var alreadyHit = position.Hit;
                if (!alreadyHit)
                {
                    gameBoard.LaunchTorpedo(position, gameBoard.Player2, gameBoard.Player1);
                    GameState();
                    if (gameBoard.Player1.Ships.RemainingShips() == 0)
                    {
                        bool player1Won = false;
                        GameOver(player1Won);
                    }
                    else
                    {
                        GoToNextPlayer();
                    }
                }

            }
        }

        public void GoToNextPlayer()
        {
            NextPlayerGrid.Visibility = Visibility.Visible;
            GameBoardGrid.Visibility = Visibility.Collapsed;
        }


        private Position GetAttackedPosition(Point point, bool player1Attacked)
        {
            var x = (int)(point.X / unitX);
            var y = (int)(point.Y / unitY);
            if (player1Attacked)
            {
                return gameBoard.GameMap2.Positions[x, y];
            }
            else
            {
                return gameBoard.GameMap1.Positions[x, y];
            }
        }

        private void GameOver(bool player1Won)
        {
            gameOver = true;
            GameOverGrid.Visibility = Visibility.Visible;

            LeftCanvas.Children.Clear();
            RightCanvas.Children.Clear();
            for (int x = 0; x < GameSize; x++)
            {
                for (int y = 0; y < GameSize; y++)
                {
                    DrawPlayerPosition(x, y, gameBoard.GameMap2.Positions[x, y], RightCanvas);
                    DrawPlayerPosition(x, y, gameBoard.GameMap1.Positions[x, y], LeftCanvas);
                }
            }

            if (player1Won)
            {
                GameOverLabel.Content = gameBoard.Player1.Name + " Won the game";
            }
            else
            {
                GameOverLabel.Content = gameBoard.Player2.Name + " Won the game";
            }
        }

        private void NextPlayerButtonClick(object sender, RoutedEventArgs e)
        {
            NextPlayerGrid.Visibility = Visibility.Collapsed;
            GameBoardGrid.Visibility = Visibility.Visible;
        }


    }

}
