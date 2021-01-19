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

        #region Base Methods
        public MainView()
        {
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;

            MainViewModel.onNewFileUploaded += LoadUploadNewFileWindow;
            InitializeComponent();
        }
        #endregion

        #region Other Methods
        private void DropNewFile(object sender, System.Windows.DragEventArgs e)
        {   
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileStrings = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string fileString in fileStrings)
                {
                    mainViewModel.AddFile(new SpreadsheetFile(fileString));
                }
            }
        }

        private void LoadUploadNewFileWindow(string fileName)
        {
            mainViewModel.AddFile(new SpreadsheetFile(fileName));
        }
        #endregion
    }
}
