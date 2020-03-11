using PSK.Model.BussinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Restriction
    {
        public int Id { set; get; }
        public int ConsecutiveDays { set; get; }
        public int MaxDaysPerYear { set; get; }
        public int MaxDaysPerQuarter { set; get; }

        public int MaxDaysPerMonth { set; get; }

        public bool Global { set; get; }

        public List<Employee> Employees { set; get; }
    }
}
