using Inventur.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.Services
{
    public interface IDataService
    {
        bool DeleteData(InventurItem item);
        List<InventurItem> GetData();
        bool SaveData(InventurItem item, bool isNew = false);
    }

    public class DataService : IDataService
    {
        public bool SaveData(InventurItem item, bool isNew = false)
        {
            bool ret = false;
                     
            return ret;
        }

        public bool DeleteData(InventurItem item) {
            bool ret = false;

            return ret;
        }

        public List<InventurItem> GetData() {
            var ret = new List<InventurItem>();

            return ret;
        }
    }
}
