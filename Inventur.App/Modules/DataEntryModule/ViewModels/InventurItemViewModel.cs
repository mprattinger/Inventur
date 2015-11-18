using GalaSoft.MvvmLight;
using Inventur.App.ViewModel;
using Inventur.Data.Models;
using Inventur.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.App.Modules.DataEntryModule.ViewModels
{
    public class InventurItemViewModel : InventurViewModelBase
    {

        private InventurItem _item;
        private bool _update;
        private IDataService _dataService;

        #region Errors
        private const string ARTICLE_ID_ERROR_EMPTY = "Bitte geben Sie eine Artikelnummer ein!";
        private const string PIECE_ERROR_EMPTY = "Bitte geben Sie eine Stückzahl ein!";
        private const string PIECE_ERROR_NONUM = "Bitte nur Zahlen als Stückzahl eingeben!";
        private const string PIECE_ERROR_ISZERO = "Stückzahl muss größer 0 sein";
        #endregion

        #region Properties
        private string _articleId;
        public string ArticleId
        {
            get { return _articleId; }
            set
            {
                if (_articleId == value) return;
                IsArticleIdValid(value);
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
                IsPieceValid(value);
                _piece = value;
                RaisePropertyChanged("Piece");
            }
        }
        #endregion

        #region Constructors
        public InventurItemViewModel(IDataService dataService) {
            _dataService = dataService;
            _item = new InventurItem();
            ArticleId = String.Empty;
            Piece = String.Empty;
        }
        public InventurItemViewModel(IDataService dataService, InventurItem item, bool update = true)
        {
            _item = item;
            _update = update;
            _dataService = dataService;
        }
        #endregion


        public static InventurItemViewModel Factory(IDataService dataService)
        {
            return new InventurItemViewModel(dataService);
        }

        #region Functions
        public bool Save()
        {
            var ret = false;

            _item.EANCode = ArticleId;
            int amount;
            int.TryParse(Piece, out amount);
            _item.Amount = amount;

            if (!_update)
            {
                //Neues
                _dataService.SaveData(_item, true);
            }
            else
            {
                //Updaten
                _dataService.SaveData(_item);

            }
            return ret;
        }
        public bool Delete()
        {
            var ret = false;

            return ret;
        }
        #endregion

        #region PropertyValidation
        public bool IsArticleIdValid(string value)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(value))
            {
                AddError("ArticleId", ARTICLE_ID_ERROR_EMPTY);
                isValid = false;
            }
            else RemoveError("ArticleId", ARTICLE_ID_ERROR_EMPTY);

            return isValid;
        }
        public bool IsPieceValid(string value)
        {
            bool isValid = true;
            int n;

            if (string.IsNullOrEmpty(value))
            {
                AddError("Piece", PIECE_ERROR_EMPTY);
                isValid = false;
            }
            else { RemoveError("Piece", PIECE_ERROR_EMPTY); }

            if (!int.TryParse(value, out n))
            {
                AddError("Piece", PIECE_ERROR_NONUM);
                isValid = false;
            }
            else { RemoveError("Piece", PIECE_ERROR_NONUM); }

            if (int.TryParse(value, out n))
            {
                if(n==0)
                {
                    AddError("Piece", PIECE_ERROR_ISZERO);
                    isValid = false;
                }
                else { RemoveError("Piece", PIECE_ERROR_ISZERO); }
            }
            

            return isValid;
        }
        #endregion
    }
}
