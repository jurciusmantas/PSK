using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSK.Model.BusinessEntities
{
    public class Topic
    {
        public int Id { set; get; }
        [Column(TypeName = "varchar(255)")]
        public String Name { set; get; }
        [Column(TypeName = "varchar(12000)")]
        public String Description { set; get; }
        public int ParentId { get; set; }
        public Topic ParentTopic { set; get; }
    }
}
