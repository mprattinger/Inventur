using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inventur.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Properties
        /// <summary>
        /// The <see cref="ArticleId" /> property's name.
        /// </summary>
        public const string ArticleIdPropertyName = "ArticleId";

        private string _preArticleId = string.Empty;
        private string _articleId = string.Empty;

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

                _articleId = value;
                RaisePropertyChanged(ArticleIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Piece" /> property's name.
        /// </summary>
        public const string PiecePropertyName = "Piece";

        private string _prePiece = string.Empty;
        private string _piece = string.Empty;

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

                _piece = value;
                RaisePropertyChanged(PiecePropertyName);
            }
        }

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
        /// The <see cref="ClearDeleteName" /> property's name.
        /// </summary>
        public const string ClearDeleteNamePropertyName = "ClearDeleteName";

        private string _clearDeleteName = "Inhalt löschen";

        /// <summary>
        /// Sets and gets the ClearDeleteName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ClearDeleteName
        {
            get
            {
                return _clearDeleteName;
            }

            set
            {
                if (_clearDeleteName == value)
                {
                    return;
                }

                _clearDeleteName = value;
                RaisePropertyChanged(ClearDeleteNamePropertyName);
            }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Clear { get; set; }
        public RelayCommand<InventurItemModel> ShowDetail { get; set; }
        #endregion
        #region ErrorHandling
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "ArticleId":
                        if (this.ArticleId == _preArticleId) break;
                        if (string.IsNullOrEmpty(this.ArticleId)) result = "Bitte geben Sie eine Artikelnummer ein!";
                        break;
                    case "Piece":
                        if (this.Piece == _prePiece) break;
                        double n;
                        if (string.IsNullOrEmpty(this.Piece)) result = "Bitte geben Sie eine Stückzahl ein!";
                        else if (!double.TryParse(this.Piece, out n)) result = "Bitte nur Zahlen als Stückzahl eingeben!";
                        _prePiece = this.Piece;
                        break;
                };
                return result;
            }
        }
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            #region Commands
            this.Add = new RelayCommand(() =>
            {
                this.AddItem();
            });
            this.Clear = new RelayCommand(() =>
            {

            });
            this.ShowDetail = new RelayCommand<InventurItemModel>((i)=> {
                this.ArticleId = i.ArticleId;
                this.Piece = i.Piece;
            });
            #endregion
        }

        public void AddItem() {
            var item = new InventurItemModel();
            item.ArticleId = this.ArticleId;
            item.Piece = this.Piece;
            this.Invented.Add(item);
        }
    }
}