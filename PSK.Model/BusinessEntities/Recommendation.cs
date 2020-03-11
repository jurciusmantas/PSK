using PSK.Model.BussinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Recommendation
    {
        public int Id { set; get; }
        public Topic Topic { set; get; }
        public Employee RecommendedTo { set; get; }

        public Employee CreatedBy { set; get; }
    }
}
