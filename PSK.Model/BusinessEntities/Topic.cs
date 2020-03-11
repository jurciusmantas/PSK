using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Topic
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public Topic ParentTopic { set; get; }
    }
}
