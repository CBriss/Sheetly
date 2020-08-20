using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sheetly.ViewModels
{
    class FileViewModel : BaseViewModel
    {
        private ObservableCollection<File> _fileList = new ObservableCollection<File>();
        public ObservableCollection<File> FileList
        {
            get { return _fileList; }
            set
            {
                _fileList = value;
                OnPropertyChanged("FileList");
            }
        }

        public void AddFile(string filePath)
        {
            try
            {
                File newFile = new File(filePath);
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
