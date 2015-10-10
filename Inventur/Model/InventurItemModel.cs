using GalaSoft.MvvmLight;
using Inventur.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Model
{
    public class InventurItemModel : ViewModelBaseClass
    {
        //public string ArticleId { get; set; }
        //public string Piece { get; set; }

        #region Properties
        /// <summary>
        /// The <see cref="ArticleId" /> property's name.
        /// </summary>
        public const string ArticleIdPropertyName = "ArticleId";

        private string _preArticleId = string.Empty;
        private string _articleId = String.Empty;

        /// <summary>
        /// Sets and gets the ArticleId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ArticleId
        {
            get
            {
                return _articleId;
            }

            set
            {
                if (_articleId == value)
                {
                    return;
                }
                IsArticleIdValid(value);

                _articleId = value;
                RaisePropertyChanged(ArticleIdPropertyName);
            }
        }
        //ERROR
        private const string ARTICLE_ID_ERROR_EMPTY = "Bitte geben Sie eine Artikelnummer ein!";

        /// <summary>
        /// The <see cref="Piece" /> property's name.
        /// </summary>
        public const string PiecePropertyName = "Piece";

        private string _prePiece = string.Empty;
        private string _piece = String.Empty;

        /// <summary>
        /// Sets and gets the Piece property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Piece
        {
            get
            {
                return _piece;
            }

            set
            {
                if (_piece == value)
                {
                    return;
                }
                IsPieceValid(value);

                _piece = value;
                RaisePropertyChanged(PiecePropertyName);
            }
        }
        //ERROR
        private const string PIECE_ERROR_EMPTY = "Bitte geben Sie eine Stückzahl ein!";
        private const string PIECE_ERROR_NONUM = "Bitte nur Zahlen als Stückzahl eingeben!";
        #endregion
        #region PropertyValidation
        public bool IsArticleIdValid(string value)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(value))
            {
                AddError(ArticleIdPropertyName, ARTICLE_ID_ERROR_EMPTY, false);
                isValid = false;
            }
            else RemoveError(ArticleIdPropertyName, ARTICLE_ID_ERROR_EMPTY);

            return isValid;
        }

        public bool IsPieceValid(string value)
        {
            bool isValid = true;
            double n;

            if (string.IsNullOrEmpty(value))
            {
                AddError(PiecePropertyName, PIECE_ERROR_EMPTY, false);
                isValid = false;
            }
            else RemoveError(PiecePropertyName, PIECE_ERROR_EMPTY);

            if (!double.TryParse(value, out n))
            {
                AddError(PiecePropertyName, PIECE_ERROR_NONUM, false);
                isValid = false;
            }
            else RemoveError(PiecePropertyName, PIECE_ERROR_NONUM);

            return isValid;
        }
        #endregion
    }
}
