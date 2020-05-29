using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PSK.Model.Entities
{
    public class Topic
    {
        public int Id { set; get; }
        [Column(TypeName = "varchar(255)")]
        public string Name { set; get; }
        [Column(TypeName = "varchar(12000)")]
        public string Description { get; set; }
        public int? ParentTopicId { get; set; }
        public virtual Topic ParentTopic { set; get; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
