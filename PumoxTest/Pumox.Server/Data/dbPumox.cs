using Pumox.Server.Data.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pumox.Server.Data
{
    public class dbPumox : DbContext
    {
        public dbPumox()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["dbPumox"].ConnectionString;
        }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
