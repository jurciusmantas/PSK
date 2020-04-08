using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class AssignedTopicSqlRepository : IAssignedTopicRepository
    {
        private readonly PSKDbContext context;

        public AssignedTopicSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public AssignedTopic Add(AssignedTopic assignedTopic)
        {
            context.AssignedTopics.Add(assignedTopic);
            context.SaveChanges();
            return assignedTopic;
        }

        public AssignedTopic Delete(int id)
        {
            AssignedTopic assignedTopic = context.AssignedTopics.Find(id);
            if (assignedTopic != null)
            {
                context.AssignedTopics.Remove(assignedTopic);
                context.SaveChanges();
            }
            return assignedTopic;
        }

        public AssignedTopic Get(int id)
        {
            return context.AssignedTopics.Find(id);
        }

        public AssignedTopic Update(AssignedTopic updatedAssignedTopic)
        {
            var assignedTopic = context.AssignedTopics.Attach(updatedAssignedTopic);
            assignedTopic.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedAssignedTopic;
        }
    }
}
