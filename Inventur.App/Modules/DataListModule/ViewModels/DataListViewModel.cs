using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Inventur.App.Contracts;
using Inventur.App.ViewModel;
using Inventur.Data.Models;
using Inventur.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.App.Modules.DataListModule.ViewModels
{
    public class DataListViewModel : InventurViewModelBase
    {
        readonly IDataService _dataService;

        #region Properties
        private InventurItem _currentItem;
        public InventurItem CurrentItem
        {
            get { return _currentItem; }
            set
            {
                if (_currentItem == value) return;
                var old = _currentItem;
                _currentItem = value;
                RaisePropertyChanged("CurrentItem", old, _currentItem, true);
            }
        }

        private ObservableCollection<InventurItem> _items = new ObservableCollection<InventurItem>();
        public ObservableCollection<InventurItem> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                RaisePropertyChanged("Items");
            }
        }

        public RelayCommand<InventurItem> Delete { get; private set; }
        public RelayCommand ItemChanged { get; private set; }
        #endregion

        public DataListViewModel(IDataService dataService)
        {
            _dataService = dataService;

            #region Commands
            Delete = new RelayCommand<InventurItem>(async item =>
            {
                await _dataService.DeleteDataAsync(item);
                LoadData();
            });
            ItemChanged = new RelayCommand(() =>
            {
                //Messenger.Default.Send<ItemSelectedMessage>(new ItemSelectedMessage { SelectedItem = CurrentItem });
            });
            #endregion

            #region Messaging
            Messenger.Default.Register<UpdateListMessage>(this, a => {
                LoadData();
            });
            #endregion

            LoadData();
        }

        public async void LoadData()
        {
            var data = await _dataService.GetDataAsync();
            Items = new ObservableCollection<InventurItem>(data);
        }


    }
}
