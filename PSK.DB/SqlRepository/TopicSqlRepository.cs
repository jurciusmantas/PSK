using Microsoft.EntityFrameworkCore;
using PSK.DB.Contexts;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PSK.DB.SqlRepository
{
    public class TopicSqlRepository : ITopicRepository
    {
        private readonly PSKDbContext context;

        public TopicSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public Topic Add(Topic topic)
        {
            context.Topics.Add(topic);
            context.SaveChanges();
            return topic;
        }

        public Topic Delete(int id)
        {
            Topic topic = context.Topics.Find(id);
            if(topic != null)
            {
                context.Topics.Remove(topic);
                context.SaveChanges();
            }
            return topic;
        }

        public Topic Get(int id)
        {
            return context.Topics.Find(id);
        }

        public List<Topic> Get()
        {
            return context.Topics.ToList();
        }

        public List<Topic> GetSubtopics(int id)
        {
            return context.Topics.Where(m => m.ParentTopicId == id).ToList();
        }

        public Topic Update(Topic updatedTopic)
        {
            var topic = context.Topics.Attach(updatedTopic);
            topic.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTopic;
        }
    }
}
