using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSK.Model.Entities
{
    public class EmployeesToken
    {
        public int Id { set; get; }
        public int EmployeeId { set; get; }
        public virtual Employee Employee { set; get; }
        [Column(TypeName = "varchar(255)")]
        public String Token { set; get; }
        public DateTime CreatedAt { set; get; }
        public DateTime ExpiredAt { set; get; }
    }
}
