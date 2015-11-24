using Inventur.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventur.Contracts.Models;
using LiteDB;

namespace Inventur.DataLiteDb.Services
{
    public class DataService : IDataService
    {
        const string dbName = "Inventur.db";
        const string tabName = "InventurItems";

        public Task<int> DeleteDataAsync(InventurItem item)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var db = new LiteDatabase(dbName))
                {
                    var col = db.GetCollection<InventurItem>(tabName);
                    var res = col.Delete(x => x.Id == item.Id);

                    return res;
                }
            });
        }

        public Task<List<InventurItem>> GetDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var db = new LiteDatabase(dbName))
                {
                    var col = db.GetCollection<InventurItem>(tabName);
                    return col.Find(x=>x.Exported == false).OrderByDescending(x=>x.ChangedAt).ToList();
                }
            });
        }

        public Task<int> SaveDataAsync(InventurItem item, bool isNew = false)
        {
            return Task.Factory.StartNew(() =>
            {
                item.ChangedAt = DateTime.Now;
                item.Exported = false;
                using (var db = new LiteDatabase(dbName))
                {
                    var col = db.GetCollection<InventurItem>(tabName);
                    if (isNew)
                    {
                        //Zuerst prüfen ob es bereits einen Eintrag gibt

                        var existing = col.Find(x => x.EANCode == item.EANCode && x.Exported == false).FirstOrDefault();
                        if (existing != null)
                        {
                            existing.Amount += item.Amount;
                            existing.ChangedAt = item.ChangedAt;
                            col.Update(existing);
                        }
                        else
                        {
                            item.CreatedAt = DateTime.Now;
                            var res = col.Insert(item);
                        }
                    }
                    else
                    {
                        col.Update(item);
                    }
                    return 1;
                }
            });
        }

        public Task<bool> SetExportedAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var db = new LiteDatabase(dbName))
                {
                    var res = true;
                    var col = db.GetCollection<InventurItem>(tabName);
                    var unchanged = col.Find(x => x.Exported == false).ToList();
                    for (var i = 0; i < unchanged.Count; i++)
                    {
                        var u = unchanged[i];
                        u.Exported = true;
                        res = col.Update(u);
                        if (!res) break;
                    }
                    return res;
                }
            });
        }
    }
}
