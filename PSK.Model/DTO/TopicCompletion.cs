using System;

namespace PSK.Model.DTO
{
    public class TopicCompletion
    {
        public int Id { set; get; }
        public int TopicId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CompletedOn { get; set; }
    }
}
