using Inventur.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.DataService
{
    public class InventurDataService
    {
        private InventurDbContext _db;

        public InventurDataService()
        {

        }

        public List<InventedModel> GetAll() {
            return _db.InventedItems.ToList();
        }

        public async void AddInventedItem(InventedModel item) {
            _db.InventedItems.Add(item);
            await _db.SaveChangesAsync();
        }
    }
}
