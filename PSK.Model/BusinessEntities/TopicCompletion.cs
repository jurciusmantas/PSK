using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class TopicCompletion
    {
        public int Id { set; get; }
        public int TopicId { set; get; }
        public Topic Topic {set; get;}
        public int EmployeeId { set; get; }
        public Employee Employee { set; get; }
        public DateTime CompletedOn { set; get; }
    }
}
