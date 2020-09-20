using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Sheetly.Models;

namespace Sheetly.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<File> _fileList;

        public MainViewModel() {
            _fileList = new ObservableCollection<File>();
            File.onRemoveFromList += RemoveFile;

            UploadFileViewModel.onNewFile += AddFile;
        }

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
    }
}
