using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.Models
{
    public class InventurContext : DbContext 
    {
        DbSet<InventurItem> InventurItems { get; set; }

        public InventurContext() : base("InventurDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var init = new SqliteCreateDatabaseIfNotExists<InventurContext>(modelBuilder);
            Database.SetInitializer(init);
        }
    }
}
