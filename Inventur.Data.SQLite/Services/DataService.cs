using Inventur.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventur.Contracts.Models;
using System.Data.Entity;
using System.Data.SQLite;
using Inventur.Data.SQLite.DBTool;

namespace Inventur.Data.SQLite.Services
{
    public class DataService : IDataService
    {
        const string TABNAME = "InventurItems";
        SQLiteDBTool _dbTool;

        public DataService()
        {
            _dbTool = new SQLiteDBTool();
            CreateTables(_dbTool.ConnectDb());
        }

        public async Task<int> DeleteDataAsync(InventurItem item)
        {
            //using (var db = new InventurContext())
            //{
            //    db.InventurItems.Attach(item);
            //    db.InventurItems.Remove(item);
            //    return await db.SaveChangesAsync();
            //}

            var command = new SQLiteCommand($"delete from {TABNAME} where ID={item.ID}", _dbTool.ConnectDb());
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<InventurItem>> GetDataAsync()
        {
            //using (var db = new InventurContext())
            //{
            //    return await (from i in db.InventurItems
            //                  where i.Exported == false
            //                  orderby i.ChangedAt descending
            //                  select i).ToListAsync();
            //}

            using (var command = new SQLiteCommand($"select * from {TABNAME} where Exported=0 orderby ChangedAt DESC", _dbTool.ConnectDb()))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var items = new List<InventurItem>();
                    if (await reader.ReadAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var i = new InventurItem { ID = Convert.ToInt32(reader["ID"]), CreatedAt = Convert.ToDateTime(reader["CreatedAt"]), ChangedAt = Convert.ToDateTime(reader["ChangedAt"]), EANCode = reader["EANCode"].ToString(), Amount = Convert.ToInt32(reader["Amount"]), Exported = Convert.ToBoolean(reader["Exported"]) };
                            items.Add(i);
                        }
                    }
                    return items;
                }
            }
        }


        public async Task<int> SaveDataAsync(InventurItem item, bool isNew = false)
        {
            //using (var db = new InventurContext())
            //{
            //    item.ChangedAt = DateTime.Now;
            //    if (isNew)
            //    {
            //        item.CreatedAt = DateTime.Now;
            //        db.InventurItems.Add(item);
            //    }
            //    else
            //    {
            //        db.InventurItems.Attach(item);
            //        var entry = db.Entry(item);
            //        entry.Property(x => x.EANCode).IsModified = true;
            //        entry.Property(x => x.ChangedAt).IsModified = true;
            //        entry.Property(x => x.Amount).IsModified = true;
            //    }
            //    return await db.SaveChangesAsync();
            //}

            SQLiteCommand command;
            item.ChangedAt = DateTime.Now;
            if (isNew)
            {
                item.CreatedAt = DateTime.Now;
                command = new SQLiteCommand($"insert into {TABNAME} (CreatedAt, ChangedAt, EANCode, Amout, Exported) values ({item.CreatedAt}, {item.ChangedAt}, {item.EANCode}, {item.Amount}, 0)", _dbTool.ConnectDb());
                return await command.ExecuteNonQueryAsync();
            }
            else
            {
                command = new SQLiteCommand($"update {TABNAME} set EANCode={item.EANCode}, ChangedAt={item.ChangedAt}, Amount={item.Amount} where ID={item.ID}", _dbTool.ConnectDb());
                return await command.ExecuteNonQueryAsync();
            }

        }

        public async Task<bool> SetExportedAsync()
        {
            //using (var db = new InventurContext())
            //{
            //    await db.InventurItems.Where(x => x.Exported == false).ForEachAsync<InventurItem>(y => y.Exported = true);
            //    var changes = await db.SaveChangesAsync();
            //    if (changes > 0) return true;
            //    return false;
            //}

            var command = new SQLiteCommand($"update {TABNAME} set Exported=1 where Exported=0", _dbTool.ConnectDb());
            var changes =  await command.ExecuteNonQueryAsync();
            if (changes > 0) return true;
            return false;
        }

        private void CreateTables(SQLiteConnection conn)
        {
            var command = new SQLiteCommand($"create table if not exists {TABNAME} ID int IDENTITY(1,1) PRIMARY KEY, CreatedAt datetime, ChangedAt datetime, EANCode varchar(100), Amount int, Exported bit", conn);
            command.ExecuteNonQuery();
        }
    }
}
