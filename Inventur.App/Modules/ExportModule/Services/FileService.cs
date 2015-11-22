using Inventur.Contracts.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.App.Modules.ExportModule.Services
{
    public interface IFileService
    {
        Task<string> OpenSaveFileDialogAsync();
        Task<bool> WriteFileAsync(string target, List<InventurItem> data);
    }

    public class FileService : IFileService
    {

        public async Task<string> OpenSaveFileDialogAsync()
        {

            return await Task.Run(() =>
            {
                var diag = new SaveFileDialog();
                diag.AddExtension = true;
                diag.DefaultExt = "csv";
                diag.Filter = "CSV Datei (*.csv)|*.csv";
                diag.ShowDialog();

                return diag.FileName;
            });
        }

        public async Task<bool> WriteFileAsync(string target, List<InventurItem> data) {
            return await Task.Run(() => {
                var ret = false;

                var fi = new FileInfo(target);
                if (fi.Exists) {
                    fi.Delete();
                }

                var lines = data.Select(d => d.EANCode + ";" + d.Amount).ToList();

                using (var sw = fi.CreateText()) {
                    lines.ForEach(async l => await sw.WriteLineAsync(l));
                }
                ret = true;

                return ret;
            });
        }
    }
}
