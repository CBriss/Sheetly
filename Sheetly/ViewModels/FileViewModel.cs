using System;
using System.Collections.ObjectModel;
using System.Windows;

using Sheetly.Models;

namespace Sheetly.ViewModels
{
    class FileViewModel : BaseViewModel
    {
        public ObservableCollection<File> _fileList;
        public FileViewModel() {
            _fileList = new ObservableCollection<File>();

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
                newFile.Name = "New File";
                _fileList.Add(newFile);
                System.Diagnostics.Debug.WriteLine("# of Files: " + _fileList.Count);
                OnPropertyChanged("FileList");
                
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Exception");
            }
            
        }
    }
}
