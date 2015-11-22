﻿using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Inventur.App.Contracts;
using Inventur.App.ViewModel;
using Inventur.Contracts.Interfaces;
using Inventur.Contracts.Messaging;
using Inventur.Contracts.Models;
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
        private bool _isLoading;

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
                RaisePropertyChanged("CurrentItem");
                //RaisePropertyChanged("CurrentItem", old, _currentItem, true);
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
                if (_isLoading) return;
                Messenger.Default.Send<ItemSelectedMessage>(new ItemSelectedMessage { SelectedItem = CurrentItem });
            });
            #endregion

            #region Messaging
            Messenger.Default.Register<UpdateListMessage>(this, a => {
                LoadData();
            });
            #endregion

            if(!IsInDesignMode) LoadData();
        }

        public async void LoadData()
        {
            _isLoading = true;
            var data = await _dataService.GetDataAsync();
            Items = new ObservableCollection<InventurItem>(data);
            _isLoading = false;
        }


    }
}
