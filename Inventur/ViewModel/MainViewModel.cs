using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
//using Inventur.Data;
using Inventur.Model;
using Inventur.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Inventur.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase //, IDataErrorInfo
    {
        //private readonly IDataService _dataService;
        //private readonly IInventurDataService _db;
        private IIOService _ioService;

        #region Properties

        /// <summary>
        /// The <see cref="Invented" /> property's name.
        /// </summary>
        public const string InventedPropertyName = "Invented";

        private ObservableCollection<InventurItemModel> _invented = new ObservableCollection<InventurItemModel>();

        /// <summary>
        /// Sets and gets the Invented property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<InventurItemModel> Invented
        {
            get
            {
                return _invented;
            }

            set
            {
                if (_invented == value)
                {
                    return;
                }

                _invented = value;
                RaisePropertyChanged(InventedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentItem" /> property's name.
        /// </summary>
        public const string CurrentItemPropertyName = "CurrentItem";

        private InventurItemModel _currentItem = new InventurItemModel();

        /// <summary>
        /// Sets and gets the CurrentItem property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public InventurItemModel CurrentItem
        {
            get
            {
                return _currentItem;
            }

            set
            {
                if (_currentItem == value)
                {
                    return;
                }

                _currentItem = value;
                RaisePropertyChanged(CurrentItemPropertyName);
                UpdateMode = true;
            }
        }

        /// <summary>
        /// The <see cref="UpdateMode" /> property's name.
        /// </summary>
        public const string UpdateModePropertyName = "UpdateMode";

        private bool _updateMode = false;

        /// <summary>
        /// Sets and gets the UpdateMode property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool UpdateMode
        {
            get
            {
                return _updateMode;
            }

            set
            {
                if (_updateMode == value)
                {
                    return;
                }

                _updateMode = value;
                RaisePropertyChanged(UpdateModePropertyName);
            }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand Update { get; set; }
        public RelayCommand<InventurItemModel> DeleteItem { get; set; }
        public RelayCommand ExportFile { get; set; }

        #endregion Properties

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IIOService ioService)//, IInventurDataService dbService)
        {
            //this._dataService = dataService;
            this._ioService = ioService;
            this.UpdateMode = false;
            //_db = dbService;

            loadData();

            #region Commands

            this.Add = new RelayCommand(() =>
            {
                this.AddItem();
            });
            this.Clear = new RelayCommand(() =>
            {
                this.CurrentItem = new InventurItemModel();
                UpdateMode = false;
            });
            this.DeleteItem = new RelayCommand<InventurItemModel>((i) =>
            {
                this.Invented.Remove(i);
            });
            this.ExportFile = new RelayCommand(() =>
            {
                //if (!_dataService.FileExists())
                //{
                //    Messenger.Default.Send<MboxMessage>(new MboxMessage { Message = "Es wurden noch keine Artikel erfasst!", Title = "Keine Artikel" });
                //    return;
                //}
                //var fname = this._ioService.OpenSaveFileDialog();
                //if (string.IsNullOrEmpty(fname)) return;
                //if (System.IO.File.Exists(fname))
                //{
                //    Messenger.Default.Send<MboxMessage>(new MboxMessage { Message = "Zieldatei exisitert bereits! Bitte anderen Dateiname wählen oder Zieldatei löschen!", Title = "Datei bereits vorhanden" });
                //    return;
                //}
                //this._dataService.CopyToTarget(fname);
                //if (System.IO.File.Exists(fname))
                //{
                //    Messenger.Default.Send<MboxMessage>(new MboxMessage { Message = "Datei wurde am Zielort angelegt!", Title = "Export beendet" });
                //}
                //else
                //{
                //    Messenger.Default.Send<MboxMessage>(new MboxMessage { Message = "Beim anlegen der Zieldatei ist ein Fehler aufgetreten! Bitte kontaktieren Sie Ihren Administrator ", Title = "Export fehler" });
                //    return;
                //}

                //_dataService.Init();
                this.CurrentItem = new InventurItemModel();
                UpdateMode = false;
                loadData();
                Messenger.Default.Send<SetFocusArticleIdMessage>(new SetFocusArticleIdMessage());
            });

            #endregion Commands
        }

        public void AddItem()
        {
            //Gibt es Fehler?
            if (this.CurrentItem.HasError()) return;

            if (!this.UpdateMode) addItem();

            this.CurrentItem = new InventurItemModel();
            UpdateMode = false;

            updateData();

            Messenger.Default.Send<SetFocusArticleIdMessage>(new SetFocusArticleIdMessage());
        }

        private void addItem()
        {
            //Zuerst prüfen ob es diesen Artikel schon in der Liste gibt:
            var exists = this.Invented.FirstOrDefault(i => i.ArticleId == this.CurrentItem.ArticleId);
            if (exists != null)
            {
                //Werte werden als String gespeichert -> Zuerst in eine Zahl umwandeln
                double current, update;
                double.TryParse(exists.Piece, out current);
                double.TryParse(this.CurrentItem.Piece, out update);
                current += update;
                exists.Piece = current.ToString();
            }
            else
            {
                this.Invented.Add(this.CurrentItem);
            }
        }

        private  void updateData()
        {
            //await _dataService.UpdateData(Invented);
            //var data = CurrentItem.GetDbModel();
            //await _db.UpdateInventedItem(data);
        }

        private void loadData()
        {
            //this.Invented = _dataService.GetData();
            //var data = _db.GetAll().Select(d => new InventurItemModel(d));
            //this.Invented = new ObservableCollection<InventurItemModel>(data);
        }
    }
}