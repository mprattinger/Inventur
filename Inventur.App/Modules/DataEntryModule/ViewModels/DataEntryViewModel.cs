using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
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

namespace Inventur.App.Modules.DataEntryModule.ViewModels
{
    public class DataEntryViewModel : InventurViewModelBase
    {
        readonly IDataService _dataService;

        #region Properties
        private InventurItemViewModel _currentItem;
        public InventurItemViewModel CurrentItem
        {
            get
            {
                return _currentItem;
            }
            set
            {
                if (_currentItem == value) return;
                _currentItem = value;
                RaisePropertyChanged("CurrentItem");
            }
        }

        public RelayCommand Add { get; private set; }
        public RelayCommand Clear { get; private set; }
        #endregion

        public DataEntryViewModel(IDataService dataService)
        {
            _dataService = dataService;

            #region Commands
            Add = new RelayCommand(async () =>
            {
                CurrentItem.CheckValid();
                if (CurrentItem.HasError()) return;

                await CurrentItem.Save();

                init();
            });
            Clear = new RelayCommand(init);
            #endregion

            #region Messages
            Messenger.Default.Register<ItemSelectedMessage>(this, item =>
            {

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    CurrentItem = new InventurItemViewModel(_dataService, item.SelectedItem);
                });
            });
            #endregion

            if (!this.IsInDesignMode) init();
        }

        private void init()
        {
            CurrentItem = InventurItemViewModel.Factory(_dataService);
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }
    }
}
