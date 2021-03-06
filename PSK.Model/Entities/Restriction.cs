﻿using System;
using System.Collections.Generic;

namespace PSK.Model.Entities
{
    public class Restriction
    {
        public int Id { set; get; }
        public int ConsecutiveDays { set; get; }
        public int MaxDaysPerYear { set; get; }
        public int MaxDaysPerQuarter { set; get; }
        public int MaxDaysPerMonth { set; get; }
        public bool Global { set; get; }
        public DateTime CreationDate { set; get; }
        public int CreatorId { set; get; }
        public virtual Employee Creator { set; get; }
        public virtual ICollection<EmployeeRestriction> RestrictionEmployees { set; get; }
    }
}
