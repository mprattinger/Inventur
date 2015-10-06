using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Services
{
    public interface IIOService
    {
        string OpenSaveFileDialog();
    }

    public class IOService : IIOService
    {
        public string OpenSaveFileDialog()
        {
            var diag = new SaveFileDialog();
            diag.AddExtension = true;
            diag.DefaultExt = "csv";
            diag.Filter = "CSV Datei(*.csv)|*.csv";
            diag.ShowDialog();

            return diag.FileName;
        }
    }
}
