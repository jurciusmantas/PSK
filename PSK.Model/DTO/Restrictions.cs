using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.DTO
{
    public class Restrictions
    {
        public Restriction Restriction { get; set; }
        public List<Restriction> RestrictionsList { get; set; }
        public List<Employee> TeamMembers { get; set; }
    }
}
