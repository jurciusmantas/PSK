using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Employee
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public String Email { set; get; }
        public String Password { set; get; }
        public Employee Leader { set; get; }
        public List<Restriction> Restrictions { set; get; }
        public String Token { set; get; }
    }
}
