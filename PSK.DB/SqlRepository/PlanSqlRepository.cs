using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.SqlRepository
{
    public class PlanSqlRepository : IPlanRepository
    {
        private readonly PSKDbContext context;

        public PlanSqlRepository(PSKDbContext context)
        {
            this.context = context;
        }

        public Plan Add(Plan plan)
        {
            context.Plans.Add(plan);
            context.SaveChanges();
            return plan;
        }

        public Plan Delete(int id)
        {
            Plan plan = context.Plans.Find(id);
            if(plan != null)
            {
                context.Plans.Remove(plan);
                context.SaveChanges();
            }
            return plan;
        }

        public Plan Get(int id)
        {
            return context.Plans.Find(id);
        }

        public Plan Update(Plan updatedPlan)
        {
            var plan = context.Plans.Attach(updatedPlan);
            plan.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedPlan;
        }
    }
}
