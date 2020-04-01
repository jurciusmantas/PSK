using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.MockRepository
{
    public class AssignedTopicMockRepository : IAssignedTopicRepository
    {
        private static List<AssignedTopic> _assignedTopics = new List<AssignedTopic>();
        public AssignedTopic Add(AssignedTopic assignedTopic)
        {
            assignedTopic.Id = _assignedTopics.Count;
            _assignedTopics.Add(assignedTopic);
            return assignedTopic;
        }

        public AssignedTopic Delete(int id)
        {
            AssignedTopic assignedTopic = _assignedTopics.Find(assignedTopic => assignedTopic.Id == id);
            if(assignedTopic != null)
            {
                _assignedTopics.Remove(assignedTopic);
            }
            return assignedTopic;
        }

        public AssignedTopic GetAssignedTopic(int id)
        {
            return _assignedTopics.Find(assignedTopic => assignedTopic.Id == id);
        }

        public AssignedTopic Update(AssignedTopic updatedAssignedTopic)
        {
            AssignedTopic assignedTopic = Delete(updatedAssignedTopic.Id);
            if(assignedTopic != null)
            {
                _assignedTopics.Add(updatedAssignedTopic);
                _assignedTopics = _assignedTopics.OrderBy(assignedTopic => assignedTopic.Id).ToList();
            }
            return assignedTopic;
        }
    }
}
