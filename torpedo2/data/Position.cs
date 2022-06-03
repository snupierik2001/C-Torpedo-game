using System;
using System.Collections.Generic;
using System.Text;

namespace torpedo2.data
{
    public class Position
    {
        public bool Hit { get; set; }
        public int Ship { get; set; }

        public Position()
        {
            Hit = false;
            Ship = 0;
        }
    }
}
