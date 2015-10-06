using Inventur.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Services
{
    public interface IDataService
    {
        ObservableCollection<InventurItemModel> GetData();
        Task UpdateData(ObservableCollection<InventurItemModel> data);
    }

    public class DataService : IDataService
    {
        private const string dataFileName = "inventur.csv";
        private string baseDir = "";
        private string fileName = "";
        private ObservableCollection<InventurItemModel> _items;
        private int count = 0;

        public DataService()
        {
            this._items = new ObservableCollection<InventurItemModel>();
            baseDir = getBaseDirectory();
            baseDir = Path.Combine(baseDir, "data");
            fileName = Path.Combine(baseDir, dataFileName);
            loadDataFile();
        }

        public ObservableCollection<InventurItemModel> GetData()
        {
            return _items;
        }

        public Task UpdateData(ObservableCollection<InventurItemModel> data)
        {
            return Task.Run(async () =>
            {
                count++;

                var fi = new FileInfo(fileName);

                if (fi.Exists)
                {
                    await fi.DeleteAsync();
                }

                var lines = data.Select(i => i.ArticleId + ";" + i.Piece).ToList();

                using (var sw = fi.CreateText()) {
                    lines.ForEach(l => sw.WriteLine(l));
                }
            });
        }

        private void loadDataFile()
        {
            if (File.Exists(fileName))
            {
                //Datei Laden
                var lines = File.ReadAllLines(fileName).ToList();
                lines.ForEach(l=>{
                    var itm = new InventurItemModel();
                    var spli = l.Split(';');
                    itm.ArticleId = spli.First();
                    itm.Piece = spli.Last();
                    _items.Add(itm);
                });
            }
            else {
                if (!Directory.Exists(baseDir)) {
                    Directory.CreateDirectory(baseDir);
                }
            }
        }

        private string getBaseDirectory() {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
