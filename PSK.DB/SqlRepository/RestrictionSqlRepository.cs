using PSK.DB.Contexts;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

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

        public List<Restriction> Get()
        {
            return context.Restrictions.ToList();
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
