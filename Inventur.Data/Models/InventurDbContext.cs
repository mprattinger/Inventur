using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.Models
{
    public class InventurDbContext  : DbContext
    {
        public DbSet<InventedModel> InventedItems { get; set; }
    }
}
