using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Employee
    {
        public int Id { set; get; }
        [Column(TypeName = "varchar(255)")]
        public String Name { set; get; }
        [Column(TypeName = "varchar(255)")]
        public String Email { set; get; }
        [Column(TypeName = "varchar(255)")]
        public String Password { set; get; }
        public int? LeaderId { get; set; }
        public Employee Leader { set; get; }
        public virtual ICollection<EmployeeRestriction> EmployeeRestrictions { set; get; }
    }
}
