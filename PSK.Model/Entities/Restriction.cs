using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Entities
{
    public class Restriction
    {
        public int Id { get; set; }
        public int ConsecutiveDays { get; set; }
        public int MaxDaysPerYear { get; set; }
        public int MaxDaysPerQuarter { get; set; }
        public int MaxDaysPerMonth { get; set; }
        public bool Global { get; set; }
        public DateTime CreationDate { get; set; }
        public int UseCount { get; set; }
    }
}
