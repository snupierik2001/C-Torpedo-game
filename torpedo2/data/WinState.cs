using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace torpedo2.data
{
    public class WinState
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Winner { get; set; }
        public int RoundsPlayed { get; set; }
        public int Player1Hits { get; set; }
        public int Player2Hits { get; set; }
        public int Player1RemainingShips { get; set; }
        public int Player2RemainingShips { get; set; }

        public WinState(Player player1, Player player2, bool player1Won, int rounds)
        {
            Player1 = player1.Name;
            Player2 = player2.Name;
            if (player1Won)
            {
                Winner = player1.Name;
            }
            else
            {
                Winner = player2.Name;
            }
            RoundsPlayed = rounds;
            Player1Hits = player1.Hits;
            Player1RemainingShips = player1.Ships.RemainingShips();
            Player2Hits = player2.Hits;
            Player2RemainingShips = player2.Ships.RemainingShips(); ;
        }

        [JsonConstructor]
        public WinState(string player1, string player2, string winner, int roundsPlayed, int player1Hits, int player2Hits, int player1RemainingShips, int player2RemainingShips)
        {
            Player1 = player1;
            Player2 = player2;
            Winner = winner;
            RoundsPlayed = roundsPlayed;
            Player1Hits = player1Hits;
            Player2Hits = player2Hits;
            Player1RemainingShips = player1RemainingShips;
            Player2RemainingShips = player2RemainingShips;
        }
    }
}
