using Inventur.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.Services
{
    public interface IDataService
    {
        Task<int> DeleteDataAsync(InventurItem item);
        Task<List<InventurItem>> GetDataAsync();
        Task<int> SaveDataAsync(InventurItem item, bool isNew = false);
    }

    public class DataService : IDataService
    {
        //private InventurContext db = new InventurContext();*/

        public async Task<int> SaveDataAsync(InventurItem item, bool isNew = false)
        {
            using (var db = new InventurContext())
            {
                item.ChangedAt = DateTime.Now;
                if (isNew)
                {
                    item.CreatedAt = DateTime.Now;
                    db.InventurItems.Add(item);
                }
                else
                {
                    db.InventurItems.Attach(item);
                    var entry = db.Entry(item);
                    entry.Property(x => x.EANCode).IsModified = true;
                    entry.Property(x => x.ChangedAt).IsModified = true;
                    entry.Property(x => x.Amount).IsModified = true;
                }
                return await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteDataAsync(InventurItem item)
        {
            using (var db = new InventurContext()) {
                db.InventurItems.Attach(item);
                db.InventurItems.Remove(item);
                return await db.SaveChangesAsync();
            }
        }

        public async Task<List<InventurItem>> GetDataAsync()
        {
            using (var db = new InventurContext()) {
                return await (from i in db.InventurItems
                                  orderby i.ChangedAt
                                  select i).ToListAsync();
            }
        }
    }
}
