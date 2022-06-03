using Newtonsoft.Json;

namespace torpedo2.data
{
    public class Ships
    {
        [JsonProperty]
        private int Ship1 { get; set; }
        [JsonProperty]
        private int Ship2 { get; set; }
        [JsonProperty]
        private int Ship3 { get; set; }
        [JsonProperty]
        private int Ship4 { get; set; }
        [JsonProperty]
        private int Ship5 { get; set; }

        public Ships()
        {
            Ship1 = 1;
            Ship2 = 2;
            Ship3 = 3;
            Ship4 = 4;
            Ship5 = 5;
        }

        public int RemainingShips()
        {
            int remainingShips = 0;

            if (Ship1 != 0) ++remainingShips;
            if (Ship2 != 0) ++remainingShips;
            if (Ship3 != 0) ++remainingShips;
            if (Ship4 != 0) ++remainingShips;
            if (Ship5 != 0) ++remainingShips;

            return remainingShips;
        }

        public void ShipWasHit(int ship)
        {
            if (ship == 5) --Ship5;
            else if (ship == 4) --Ship4;
            else if (ship == 3) --Ship3;
            else if (ship == 2) --Ship2;
            else if (ship == 1) --Ship1;
        }
    }
}
