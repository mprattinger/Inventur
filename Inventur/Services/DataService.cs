using Inventur.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Services
{
    public interface IDataService
    {
        ObservableCollection<InventurItemModel> GetData();
        void UpdateData(ObservableCollection<InventurItemModel> data);
    }

    public class DataService : IDataService
    {
        private const string dataFileName = "inventur.csv";
        private string fileName = "";
        private ObservableCollection<InventurItemModel> _items;

        public DataService()
        {
            this._items = new ObservableCollection<InventurItemModel>();
            var baseDir = Assembly.GetExecutingAssembly().CodeBase;
            fileName = System.IO.Path.Combine(baseDir, "data", dataFileName);
            createAndLoadDataFile();
        }

        public ObservableCollection<InventurItemModel> GetData()
        {
            return _items;
        }

        public void UpdateData(ObservableCollection<InventurItemModel> data)
        {

        }

        private void createAndLoadDataFile()
        {
            if (System.IO.File.Exists(fileName))
            {
                //Datei Laden
                var lines = System.IO.File.ReadAllLines(fileName);
                var splittet = lines.Select(l => l.Split(';'));
            }
            else
            {
                //Datei erzeugen
                System.IO.File.CreateText(fileName);
            }
        }

    }
}
