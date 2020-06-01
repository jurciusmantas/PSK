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

        public Restriction GetLastGlobal()
        {
            return context.Restrictions.Where(r => r.Global).OrderByDescending(r => r.CreationDate).First();
        }

        public (List<Restriction>, List<List<string>>) GetRestrictionsTo(int creatorId)
        {
            var restrictions = context.Restrictions.Where(restriction => restriction.CreatorId == creatorId).ToList();
            List<DateTime> dateTimes = restrictions.Select(r => r.CreationDate).ToList();
            var useCountNames = GetActiveUseCounts(dateTimes);
            
            return (restrictions, useCountNames);
        }

        public Restriction Update(Restriction updatedRestriction)
        {
            var restriction = context.Restrictions.Attach(updatedRestriction);
            restriction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedRestriction;
        }

        private List<List<string>> GetActiveUseCounts(List<DateTime> restrictionCreationDates)
        {
            context.Database.OpenConnection();
            List<List<string>> allActiveUseCountNames = new List<List<string>>();
            using (var connection = context.Database.GetDbConnection())
            {
                var commandText = 
                    @"SELECT * FROM 
                    (   
                        SELECT Max(r.CreationDate) AS maxCrDate, e.Name
                        FROM employeerestriction AS er
                        INNER JOIN restrictions AS r ON r.Id = er.RestrictionId
                        INNER JOIN employees AS e ON e.Id = er.EmployeeId
                        GROUP BY er.EmployeeId
                    ) AS temp
                    WHERE maxCrDate = @date;";
               
                foreach (DateTime date in restrictionCreationDates)
                {
                    using var command = connection.CreateCommand();
                    command.CommandText = commandText;
                    command.Parameters.Add(new MySqlParameter("date", date));
                    var reader = command.ExecuteReader();
                    List<string> activeUseCountsNames = new List<string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(1);
                            activeUseCountsNames.Add(name);
                        }
                    }
                    allActiveUseCountNames.Add(activeUseCountsNames);
                    reader.Close();
                }
      
            }
            return allActiveUseCountNames;
            
        }
    }
}
