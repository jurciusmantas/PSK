using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Plan
    {
        public int Id { set; get; }
        public DateTime WorkDate { set; get; }
        public int AssignedTopicId { set; get; }
        public AssignedTopic AssignedTopic { set; get; }
    }
}
