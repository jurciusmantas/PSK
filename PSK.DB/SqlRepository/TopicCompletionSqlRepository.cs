﻿using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PSK.DB.SqlRepository
{
    public class TopicCompletionSqlRepository : ITopicCompletionRepository
    {
        private readonly PSKDbContext context;

        public TopicCompletionSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }

        public TopicCompletion Add(TopicCompletion topicCompletion)
        {
            context.TopicCompletions.Add(topicCompletion);
            context.SaveChanges();
            return topicCompletion;
        }

        public TopicCompletion Delete(int id)
        {
            TopicCompletion topicCompletion = context.TopicCompletions.Find(id);
            if(topicCompletion != null)
            {
                context.TopicCompletions.Remove(topicCompletion);
                context.SaveChanges();
            }
            return topicCompletion;
        }

        public TopicCompletion Get(int id)
        {
            return context.TopicCompletions.Find(id);
        }

        public List<TopicCompletion> Get()
        {
            return context.TopicCompletions.ToList();
        }

        public TopicCompletion Update(TopicCompletion updatedTopicCompletion)
        {
            var topicCompletion = context.TopicCompletions.Attach(updatedTopicCompletion);
            topicCompletion.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTopicCompletion;
        }
    }
}
