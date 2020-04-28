﻿using System;

namespace PSK.Model.Entities
{
    public class TopicCompletion
    {
        public int Id { set; get; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { set; get; }
        public DateTime CompletedOn { get; set; }
    }
}