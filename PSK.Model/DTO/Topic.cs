using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSK.Model.DTO
{
    public class Topic
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int? ParentId { set; get; }

        public List<Topic> SubTopicList { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
