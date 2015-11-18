using GalaSoft.MvvmLight;
using Inventur.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.App.Modules.DataEntryModule.ViewModels
{
    public class InventurItemViewModel : ViewModelBase
    {

        private InventurItem _item;

        #region Properties
        private string _articleId;
        public string ArticleId
        {
            get { return _articleId; }
            set
            {
                if (_articleId == value) return;
                _articleId = value;
                RaisePropertyChanged("ArticleId");
            }
        }

        private string _piece;
        public string Piece
        {
            get { return _piece; }
            set
            {
                if (_piece == value) return;
                _piece = value;
                RaisePropertyChanged("Piece");
            }
        }
        #endregion

        #region Constructors
        public InventurItemViewModel() : this(new InventurItem()) { }
        public InventurItemViewModel(InventurItem item)
        {
            _item = item;
        }
        #endregion


        public static InventurItemViewModel Factory() {
            return new InventurItemViewModel();
        }
    }
}
