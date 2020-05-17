using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Entities
{
    public class RestrictionArgs
    {
        public int ConsecutiveDays { set; get; }
        public int MaxDaysPerYear { set; get; }
        public int MaxDaysPerQuarter { set; get; }
        public int MaxDaysPerMonth { set; get; }
        public bool Global { set; get; }
        public int CreatorId { set; get; }
        public int ReceiverId { set; get; }
    }
}
