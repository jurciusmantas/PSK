using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class RestrictionSqlRepository : IRestrictionRepository
    {
        private readonly PSKDbContext context;

        public RestrictionSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public Restriction Add(Restriction restriction)
        {
            context.Restrictions.Add(restriction);
            context.SaveChanges();
            return restriction;
        }

        public Restriction Delete(int id)
        {
            Restriction restriction = context.Restrictions.Find(id);
            if(restriction != null)
            {
                context.Restrictions.Remove(restriction);
                context.SaveChanges();
            }
            return restriction;
        }

        public Restriction Get(int id)
        {
            return context.Restrictions.Find(id);
        }

        public Restriction Update(Restriction updatedRestriction)
        {
            var restriction = context.Restrictions.Attach(updatedRestriction);
            restriction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedRestriction;
        }
    }
}
