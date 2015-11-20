using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventur.App.Contracts
{
    public delegate Task AsyncAction();

    public class RelayCommandAsync : ICommand
    {
        readonly AsyncAction _handler;

        public RelayCommandAsync(AsyncAction handler)
        {
            _handler = handler;
            IsEnabled = true;
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ExecuteAsync();
        }

        private Task ExecuteAsync()
        {
            return _handler();
        }
    }
}
