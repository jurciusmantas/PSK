using PSK.DB.Contexts;
using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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
            throw new NotImplementedException();
        }

        public (List<Restriction>, List<int>) GetRestrictionsTo(int creatorId)
        {
            var restrictions = context.Restrictions.Where(restriction => restriction.CreatorId == creatorId).ToList();
            List<DateTime> dateTimes = restrictions.Select(r => r.CreationDate).ToList();
            var useCounts = GetActiveUseCounts(dateTimes);
            
            return (restrictions, useCounts);
        }

        public Restriction Update(Restriction updatedRestriction)
        {
            var restriction = context.Restrictions.Attach(updatedRestriction);
            restriction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedRestriction;
        }

        private List<int> GetActiveUseCounts(List<DateTime> restrictionCreationDates)
        {
            context.Database.OpenConnection();
            List<int> activeUseCounts = new List<int>();
            using (var connection = context.Database.GetDbConnection())
            {
                using var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT COUNT(*) FROM 
                    (   
                        SELECT Max(r.CreationDate) AS maxCrDate
                        FROM employeerestriction AS er
                        INNER JOIN restrictions AS r ON r.Id = er.RestrictionId
                        GROUP BY er.EmployeeId
                    ) AS temp
                    WHERE maxCrDate = @date;";
                foreach(DateTime date in restrictionCreationDates)
                {
                    if(command.Parameters.Count == 0)
                    {
                        command.Parameters.Add(new MySqlParameter("date", date));
                    }
                    else
                    {
                        command.Parameters[0].Value = date;
                    }
                    var activeUseCount = command.ExecuteScalar().ToString();
                    activeUseCounts.Add(int.Parse(activeUseCount));
                }
            }
            return activeUseCounts;
            
        }
    }
}
