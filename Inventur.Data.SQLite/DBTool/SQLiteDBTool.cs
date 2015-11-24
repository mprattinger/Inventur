using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.SQLite.DBTool
{
    public class SQLiteDBTool
    {
        const string dbFile = @".\data\user.sqlite";

        private SQLiteConnection _conn;

        public void CheckOrCreateDb()
        {
            var dbFileInfo = new FileInfo(dbFile);
            dbFileInfo.Directory.Create();
        }

        public SQLiteConnection ConnectDb(bool renew = false)
        {
            CheckOrCreateDb();
            if (_conn == null || renew == true)
            {
                _conn = new SQLiteConnection($"Data Source={dbFile}; Version=3");
                _conn.Open();
            }
            return _conn;
        }
    }
}
