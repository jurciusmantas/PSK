using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Entities
{
    public class Topic
    {
        public string Name { set; get; }
        public string Description { set; get; }

        public List<Topic> SubTopicList { get; set; }
    }
}
