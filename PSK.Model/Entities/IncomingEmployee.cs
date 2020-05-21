using System.ComponentModel.DataAnnotations.Schema;

namespace PSK.Model.Entities
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
