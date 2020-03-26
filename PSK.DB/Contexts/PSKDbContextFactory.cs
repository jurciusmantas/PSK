using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.Contexts
{
    public class PSKDbContextFactory : IDesignTimeDbContextFactory<PSKDbContext>
    {
        public PSKDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PSKDbContext>();
            optionsBuilder.UseMySql("Server=localhost;Database=PSK_DB;Password=Lukas123456;User=root");

            return new PSKDbContext(optionsBuilder.Options);
        }
    }
}
