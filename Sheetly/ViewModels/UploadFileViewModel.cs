using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Sheetly.Models;

namespace Sheetly.ViewModels
{
    class UploadFileViewModel : BaseViewModel
    {
        public static Action<File> onNewFile;

        private File newFile;
        public File NewFile
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

        public UploadFileViewModel()
        {
            submitFile = new Command(SubmitFileCommand);
            setFile = new Command(setNewFile);
        }

        private void SubmitFileCommand(object sender)
        {
            onNewFile(NewFile);
        }

        private void setNewFile(object fileName)
        {
            NewFile = new File((string)fileName);
        }

        private void indexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NewFile.IndexColumn = (string)cmb.SelectedItem;
        }
    }
}
