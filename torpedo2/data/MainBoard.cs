using System;
using System.Collections.Generic;
using System.Text;

namespace torpedo2.data
{
    class MainBoard
    {
        public GameMap GameMap1 { get; set; }
        public GameMap GameMap2 { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public int Turn { get; set; }

        public bool TurnStarts { get; set; }

        public MainBoard(string name1, bool human1, string name2, bool human2)
        {
            GameMap1 = new GameMap();
            GameMap2 = new GameMap();

            Player1 = new Player(name1, human1);
            Player2 = new Player(name2, human2);

            Turn = 1;
            TurnStarts = true;
        }

        public void LaunchTorpedo(Position position, Player attacker, Player attacked)
        {
            position.Hit = true;
            attacked.Ships.ShipWasHit(position.Ship);
            ++attacker.Fires;
            if (position.Ship>0)
            {
                ++attacker.Hits;
            }
            if (!TurnStarts)
            {
                ++Turn;
                TurnStarts = true;
            }
            else
            {
                TurnStarts = false;
            }
        }
    }
}
