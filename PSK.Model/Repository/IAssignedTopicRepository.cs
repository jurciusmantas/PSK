using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IAssignedTopicRepository
    {
        public AssignedTopic Add(AssignedTopic assignedTopic);
        public AssignedTopic GetAssignedTopic(int id);
        public AssignedTopic Update(AssignedTopic updatedAssignedTopic);
        public AssignedTopic Delete(int id);

    }
}
