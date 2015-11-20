using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Inventur.App.Contracts;
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

namespace Inventur.App
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                Messenger.Default.Register<MboxMessage>(this, (msg) => {
                    //DispatcherHelper.CheckBeginInvokeOnUI(()=> {
                    //    MessageBox.Show(msg.Message, msg.Title);
                    //});
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(msg.Message, msg.Title);
                    });
                });
            };
        }
    }
}
