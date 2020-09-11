using System;
using System.Windows;
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

        public ICommand upload { get; set;  }
        public ICommand addFile { get; set; }

        public UploadFileViewModel()
        {
            upload = new Command(UploadNewFile);
            addFile = new Command(AddNewFile);
        }

        private void UploadNewFile(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                NewFile = new File(fileDialog.FileName);
            }
        }

        private void AddNewFile(object sender)
        {
            onNewFile(NewFile);
        }
    }
}
