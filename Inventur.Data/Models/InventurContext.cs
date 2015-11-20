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
        public DbSet<InventurItem> InventurItems { get; set; }

        public InventurContext() : base("InventurDb")
        {

        }
    }
}
