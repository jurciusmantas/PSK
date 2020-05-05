using PSK.DB.Contexts;
using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PSK.DB.SqlRepository
{
    public class DaySqlRepository : IDayRepository
    {
        private readonly PSKDbContext context;

        public DaySqlRepository(PSKDbContext context)
        {
            this.context = context;
        }
        public Day Add(Day day)
        {
            context.Days.Add(day);
            context.SaveChanges();
            return day;
        }

        public Day Delete(int id)
        {
            Day day = context.Days.Find(id);
            if (day != null)
            {
                context.Days.Remove(day);
                context.SaveChanges();
            }
            return day;
        }

        public Day Get(int id)
        {
            return context.Days.Find(id);
        }

        public List<Day> Get()
        {
            return context.Days.ToList();
        }

        public Day Update(Day updatedDay)
        {
            var day = context.Days.Attach(updatedDay);
            day.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedDay;
        }
    }
}
