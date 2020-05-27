using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.DTO
{
    public class RestrictionArgs
    {
        public int ConsecutiveDays { get; set; }
        public int MaxDaysPerYear { get; set; }
        public int MaxDaysPerQuarter { get; set; }
        public int MaxDaysPerMonth { get; set; }
        public int CreatorId { get; set; }
        public int ApplyTo { get; set; }
        public string[] UserNames { get; set; }
    }
}
