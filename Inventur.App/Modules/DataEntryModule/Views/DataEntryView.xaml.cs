using Inventur.App.Modules.DataEntryModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventur.App.Modules.DataEntryModule.Views
{
    /// <summary>
    /// Interaktionslogik für DataEntryView.xaml
    /// </summary>
    public partial class DataEntryView : UserControl
    {
        public DataEntryView()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var tb = (TextBox)sender;
                tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void TextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = (DataEntryViewModel)DataContext;
                vm.Add.Execute(null);
                var tb = (TextBox)sender;
                tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }
        }
    }
}
