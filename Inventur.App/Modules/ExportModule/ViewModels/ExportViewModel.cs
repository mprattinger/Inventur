using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Inventur.App.Contracts;
using Inventur.App.Modules.ExportModule.Services;
using Inventur.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventur.App.Modules.ExportModule.ViewModels
{
    public class ExportViewModel  : ViewModelBase
    {
        readonly IDataService _dataService;
        readonly IFileService _fileService;

        #region Properties
        public ICommand ExportData { get; set; }
        #endregion

        public ExportViewModel(IDataService dataService, IFileService fileService)
        {
            _dataService = dataService;
            _fileService = fileService;

            #region Commands
            ExportData = new RelayCommandAsync(async () => {
                //Dateiname erfragen
                var name = await fileService.OpenSaveFileDialogAsync();
                if (string.IsNullOrEmpty(name)) return;
                //if (File.Exists(name))
                //{
                //    //Notify User, dass die Datei bereits existert!
                //    Messenger.Default.Send<MboxMessage>(new MboxMessage { Title = "Datei existiert bereits!", Message = "Die Datei gibt es bereits! Bitte eine andere wählen!" });
                //    return;
                //}
                //List exportieren
                var data = await _dataService.GetDataAsync();
                if (data.Count == 0)
                {
                    Messenger.Default.Send<MboxMessage>(new MboxMessage { Title = "Keine Daten für den Export!", Message = "Es gibt keine Daten die Exportiert werden können!" });
                    return;
                }
                if (!await _fileService.WriteFileAsync(name, data))
                {
                    Messenger.Default.Send<MboxMessage>(new MboxMessage { Title = "Datei konne nicht geschrieben werden!", Message = $"Die Daten konnten nicht in die Datei {name} geschrieben werden!" });
                    return;
                }

                //Liste auf exportiert setzen
                if (!await _dataService.SetExportedAsync())
                {
                    Messenger.Default.Send<MboxMessage>(new MboxMessage { Title = "Daten konnten nicht auf exportiert gesetzt werden!", Message = "Die exportierten Daten konnten nicht auf exportiert gesetzt werden! Bitte kontaktieren Sie den Administrator!" });
                    return;
                }

                //List aktualisieren...
                Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());

                Messenger.Default.Send<MboxMessage>(new MboxMessage { Title = "Datei wurde erstellt!", Message = "Die Datei wurde erstellt!" });
            });
            #endregion
        }
    }
}
