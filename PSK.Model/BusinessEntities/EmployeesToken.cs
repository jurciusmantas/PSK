using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class EmployeesToken
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public Employee Employee { set; get; }
        [Column(TypeName = "varchar(255)")]
        public String Token { set; get; }
        public DateTime CreatedAt { set; get; }
        public DateTime ExpiredAt { set; get; }
    }
}
