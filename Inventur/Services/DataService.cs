//using Inventur.Model;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Inventur.Services
//{
//    public interface IDataService
//    {
//        ObservableCollection<InventurItemModel> GetData();
//        Task UpdateData(ObservableCollection<InventurItemModel> data);
//        void CopyToTarget(string target);
//        bool FileExists();
//        void Init();
//    }

//    public class DataService : IDataService
//    {
//        private const string dataFileName = "inventur.csv";
//        private string baseDir = "";
//        private string fileName = "";
//        private ObservableCollection<InventurItemModel> _items;

//        public DataService()
//        {
//            this._items = new ObservableCollection<InventurItemModel>();
//            baseDir = getBaseDirectory();
//            baseDir = Path.Combine(baseDir, "data");
//            fileName = Path.Combine(baseDir, dataFileName);
//            loadDataFile();
//        }

//        public ObservableCollection<InventurItemModel> GetData()
//        {
//            return _items;
//        }

//        public Task UpdateData(ObservableCollection<InventurItemModel> data)
//        {
//            return Task.Run(async () =>
//            {
//                var fi = new FileInfo(fileName);
//                if (fi.Exists)
//                {
//                    await fi.DeleteAsync();
//                }

//                var lines = data.Select(i => i.ArticleId + ";" + i.Piece).ToList();

//                using (var sw = fi.CreateText())
//                {
//#pragma warning disable RECS0002 // Convert anonymous method to method group
//                        lines.ForEach(l => sw.WriteLine(l));
//#pragma warning restore RECS0002 // Convert anonymous method to method group
//                    }
//            });
//        }

//        private void loadDataFile()
//        {
//            if (File.Exists(fileName))
//            {
//                //Datei Laden
//                var lines = File.ReadAllLines(fileName).ToList();
//                lines.ForEach(l =>
//                {
//                    var itm = new InventurItemModel();
//                    var spli = l.Split(';');
//                    itm.ArticleId = spli.First();
//                    itm.Piece = spli.Last();
//                    _items.Add(itm);
//                });
//            }
//            else
//            {
//                if (!Directory.Exists(baseDir))
//                {
//                    Directory.CreateDirectory(baseDir);
//                }
//            }
//        }

//        private string getBaseDirectory()
//        {
//            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
//            var uri = new UriBuilder(codeBase);
//            string path = Uri.UnescapeDataString(uri.Path);
//            return Path.GetDirectoryName(path);
//        }

//        public void CopyToTarget(string target)
//        {
//            var fi = new FileInfo(fileName);
//            fi.MoveTo(target);
//        }

//        public bool FileExists()
//        {
//            return File.Exists(fileName);
//        }

//        public void Init()
//        {
//            _items = new ObservableCollection<InventurItemModel>();
//            loadDataFile();
//        }
//    }
//}
