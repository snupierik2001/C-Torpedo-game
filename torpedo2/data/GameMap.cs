using System;
using System.Collections.Generic;
using System.Text;

namespace torpedo2.data
{
    public class GameMap
    {
        public Position[,] Positions { get; set; }

        private readonly int gameSize = 10;

        private readonly Random _random = new Random();

        public GameMap()
        {
            Positions = StartPositions();
            Positions = ShipsSizes(Positions);
        }

        public Position[,] StartPositions()
        {
            Position[,] positions = new Position[gameSize, gameSize];
            for (int i = 0; i < gameSize; i++)
                for (int j = 0; j < gameSize; j++)
                {
                    positions[i, j] = new Position();
                }
            return positions;
        }

        private Position[,] ShipsSizes(Position[,] positions)
        {
            int x;
            int y;
            int axis;
            for (int ship = 5; ship > 0; ship--)
            {
                var shipPosNotFound = true;
                while (shipPosNotFound)
                {
                    shipPosNotFound = false;
                    x = _random.Next(gameSize);
                    y = _random.Next(gameSize);
                    axis = _random.Next(2);

                    if (axis == 1)
                    {
                        if (x < 5)
                        {
                            for (int j = x; j < 10; j++)
                            {
                                if (positions[j, y].Ship != 0)
                                    shipPosNotFound = true;
                            }
                            if (!shipPosNotFound)
                                positions = InitializeShipPozitions(x, y, axis, 1, positions, ship);
                        }
                        else
                        {
                            for (int j = x; j > 0; j--)
                            {
                                if (positions[j, y].Ship != 0)
                                    shipPosNotFound = true;
                            }
                            if (!shipPosNotFound)
                                positions = InitializeShipPozitions(x, y, axis, -1, positions, ship);
                        }
                    }
                    else
                    {
                        axis = 2;
                        if (y < 5)
                        {
                            for (int j = y; j < 10; j++)
                            {
                                if (positions[x, j].Ship != 0)
                                    shipPosNotFound = true;
                            }
                            if (!shipPosNotFound)
                                positions = InitializeShipPozitions(x, y, axis, 1, positions, ship);
                        }
                        else
                        {
                            for (int j = y; j > 0; j--)
                            {
                                if (positions[x, j].Ship != 0)
                                    shipPosNotFound = true;
                            }
                            if (!shipPosNotFound)
                                positions = InitializeShipPozitions(x, y, axis, -1, positions, ship);
                        }
                    }
                }
            }
            return positions;
        }

        private Position[,] InitializeShipPozitions(int x, int y, int axis, int direction, Position[,] positions, int ship)
        {
            int shipSize = ship;
            if (axis == 1)
            {
                while (shipSize > 0)
                {
                    positions[x, y].Ship = ship;
                    x += direction;
                    shipSize--;
                }
            }
            else
            {
                while (shipSize > 0)
                {
                    positions[x, y].Ship = ship;
                    y += direction;
                    shipSize--;
                }
            }
            return positions;
        }
    }
}
