namespace torpedo2.data
{
    public class Player
    {
        public string Name { get; set; }
        public bool Human { get; set; }
        public int Hits { get; set; }
        public int Fires { get; set; }
        public Ships Ships;

        public Player(string name, bool human)
        {
            Name = name;
            Human = human;
            Ships = new Ships();
            Hits = 0;
            Fires = 0;
        }
    }
}