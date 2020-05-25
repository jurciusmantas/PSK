using System;

namespace PSK.Model.Entities
{
    public class Day
    {
        public int Id { set; get; }
        public DateTime Date { set; get; }
        public int TopicId { set; get; }
        public virtual Topic Topic { set; get; }
        public int EmployeeId { set; get; }
        public virtual Employee Employee { set; get; }
    }
}
