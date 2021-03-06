﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSK.Model.Entities
{
    public class Employee
    {
        public int Id { set; get; }
        [Column(TypeName = "varchar(255)")]
        public string Name { set; get; }
        [Column(TypeName = "varchar(255)")]
        public string Email { set; get; }
        [Column(TypeName = "varchar(255)")]
        public string Password { set; get; }
        public int? LeaderId { get; set; }
        public virtual Employee Leader { set; get; }
        public virtual ICollection<EmployeeRestriction> EmployeeRestrictions { set; get; }
    }
}
