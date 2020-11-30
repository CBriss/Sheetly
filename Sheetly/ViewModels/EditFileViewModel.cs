using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Sheetly.Models;

namespace Sheetly.ViewModels
{
    class EditFileViewModel : BaseViewModel
    {
        public static Action<SpreadsheetFile> onNewFile;

        #region Properties
        private SpreadsheetFile newFile;
        public SpreadsheetFile NewFile
        {
            get { return newFile; }
            set
            {
                newFile = value;
                OnPropertyChanged("NewFile");
            }
        }

        public Command submitFile { get; set; }
        public Command setFile { get; set; }
        #endregion

        #region Base Methods
        public EditFileViewModel()
        {
            submitFile = new Command(SubmitFileCommand);
            setFile = new Command(setNewFile);
        }
        #endregion

        #region Other Methods
        private void SubmitFileCommand(object sender)
        {
            onNewFile(NewFile);
        }

        private void setNewFile(object fileName)
        {
            NewFile = new SpreadsheetFile((string)fileName);
        }

        private void indexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NewFile.IndexColumn = (string)cmb.SelectedItem;
        }
        #endregion
    }
}
