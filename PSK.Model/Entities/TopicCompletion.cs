using System;

namespace PSK.Model.Entities
{
    public class TopicCompletion
    {
        public int Id { set; get; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { set; get; }
        public DateTime CompletedOn { get; set; }
    }
}
