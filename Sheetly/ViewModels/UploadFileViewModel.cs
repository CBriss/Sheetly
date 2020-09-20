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

        public Command upload { get; set;  }
        public Command addFile { get; set; }

        public UploadFileViewModel()
        {
            upload = new Command(UploadNewFile);
            addFile = new Command(AddNewFile);
        }

        private void UploadNewFile(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Accepted files |*.txt;*.csv;*.xls;*.xlsx;*.number;*.odt";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    NewFile = new File(fileDialog.FileName);
                }
                catch(ArgumentException e)
                {
                    MessageBox.Show(e.Message); ;
                }
                
            }
        }

        private void AddNewFile(object sender)
        {
            onNewFile(NewFile);
        }

        private void indexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            NewFile.IndexColumn = (string)cmb.SelectedItem;
        }
    }
}
