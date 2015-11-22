using Inventur.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Contracts.Interfaces
{
    public interface IDataService
    {
        Task<int> DeleteDataAsync(InventurItem item);
        Task<List<InventurItem>> GetDataAsync();
        Task<int> SaveDataAsync(InventurItem item, bool isNew = false);
        Task<bool> SetExportedAsync();
    }
}
