using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Sheetly.Models;
using Sheetly.Views;

namespace Sheetly.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public Command finderUpload { get; set; }

        public static Action<string> onNewFileUploaded;
        public MainViewModel() {
            _fileList = new ObservableCollection<File>();
            File.onRemoveFromList += RemoveFile;

            UploadFileViewModel.onNewFile += AddFile;

            finderUpload = new Command(UploadNewFileFromFinderCommand);
        }

        public ObservableCollection<File> _fileList;
        public ObservableCollection<File> FileList
        {
            get { return _fileList; }
            set
            {
                _fileList = value;
                OnPropertyChanged("FileList");
            }
        }

        public void AddFile(File newFile)
        {
            try
            {
                if(FileList.Count > 0)
                {
                    File lastFile = FileList.Last();
                    lastFile.Connection.NextFile = newFile;
                }
                

                FileList.Add(newFile);
                System.Diagnostics.Debug.WriteLine("# of Files: " + FileList.Count);
                OnPropertyChanged("FileList");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Exception");
            }
            
        }

        public void RemoveFile(object sender)
        {
            FileList.Remove((File)sender);
            System.Diagnostics.Debug.WriteLine("# of Files: " + FileList.Count);
            OnPropertyChanged("FileList");
        }

        private void UploadNewFileFromFinderCommand(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Accepted files |*.txt;*.csv;*.xls;*.xlsx;*.number;*.odt";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    onNewFileUploaded(fileDialog.FileName);
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message); ;
                }

            }
        }
    }
}
