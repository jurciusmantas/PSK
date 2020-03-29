﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class AssignedTopic
    {
        public int Id { set; get; }
        public bool IsCompleted { set; get; }
        public DateTime CompletedOn { set; get; }
        public Topic Topic { set; get; }
        public Employee Employee { set; get; }
    }
}