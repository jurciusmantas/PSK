using System.Collections.Generic;

namespace PSK.Model.DTO
{
    public class Topic
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int? ParentId { set; get; }

        public List<Topic> SubTopicList { get; set; }
    }
}
