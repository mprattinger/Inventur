using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.App.ViewModel
{
    public class InventurViewModelBase : ViewModelBase, IDataErrorInfo
    {
        private Dictionary<String, List<String>> errors = new Dictionary<string, List<string>>();


        #region ErrorHandling
        public void AddError(string propertyName, string error, bool isWarning = false)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(error))
            {
                if (isWarning) errors[propertyName].Add(error);
                else errors[propertyName].Insert(0, error);
            }
        }

        // Removes the specified error from the errors collection if it is present. 
        public void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(error))
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0) errors.Remove(propertyName);
            }
        }
        public bool HasError()
        {
            if (errors.Count() > 0) return true;
            return false;
        }
        #endregion

        #region IDataErrorInfo
        public string this[string propertyName]
        {
            get
            {
                return (!errors.ContainsKey(propertyName) ? null :
                   String.Join(Environment.NewLine, errors[propertyName]));
            }
        }

        public string Error
        {
            get
            {
                return String.Empty;
            }
        }
        #endregion
    }
}
