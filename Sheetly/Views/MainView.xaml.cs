using Sheetly.Models;
using Sheetly.ViewModels;
using Sheetly.Views;
using System.Windows;
using System.Windows.Controls;

namespace Sheetly
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainView : Window
    {
        MainViewModel mainViewModel;

        public MainView()
        {
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;

            MainViewModel.onNewFileUploaded += LoadUploadNewFileWindow;
            InitializeComponent();
        }

        private void FileDropStackPanel_Drop(object sender, System.Windows.DragEventArgs e)
        {
            UploadFileView newUploadWidow = new UploadFileView();
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileStrings = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string fileString in fileStrings)
                {
                    newUploadWidow.setFile(fileString);
                }
            }
            newUploadWidow.Show();
        }

        private void LoadUploadNewFileWindow(string fileName)
        {
            UploadFileView newUploadWidow = new UploadFileView();
            newUploadWidow.setFile(fileName);
            newUploadWidow.Show();
        }

    }
}
