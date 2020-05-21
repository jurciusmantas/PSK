using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PSK.Model.Entities;

namespace PSK.DB.Contexts
{
    public class PSKDbContext : DbContext
    {
        public PSKDbContext(DbContextOptions<PSKDbContext> options) : base(options)
        {
        }
        public DbSet<TopicCompletion> TopicCompletions { set; get; }
        public DbSet<Employee> Employees { set; get; }
        public DbSet<EmployeesToken> EmployeesTokens { set; get; }
        public DbSet<IncomingEmployee> IncomingEmployees { set; get; }
        public DbSet<Day> Days { set; get; }
        public DbSet<Recommendation> Recommendations { set; get; }
        public DbSet<Restriction> Restrictions { set; get; }
        public DbSet<Topic> Topics { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
