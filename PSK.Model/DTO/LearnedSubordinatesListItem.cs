using System;

namespace PSK.Model.DTO
{
    public class LearnedSubordinatesListItem
    {
        public string SubordinateName { get; set; }
        public DateTime? CompletedOn { get; set; }
        public DateTime? LearnsAt { get; set; }
    }
}
