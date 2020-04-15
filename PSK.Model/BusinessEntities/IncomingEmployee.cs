using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class IncomingEmployee
    {
        public int Id { set; get; }
        [Column(TypeName = "varchar(255)")]
        public string Email { set; get; }
        [Column(TypeName = "varchar(255)")]
        public string Token { set; get; }
        public int LeaderId { set; get; }
        public Employee Leader { set; get; }
    }
}
