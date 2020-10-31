using Sheetly.ViewModels;
using Sheetly.Models;
using System.Windows;
using System.IO;

namespace Sheetly.Views
{
    /// <summary>
    /// Interaction logic for NewFile.xaml
    /// </summary>
    public partial class UploadFileView
    {
        UploadFileViewModel uploadFileViewModel;

        public UploadFileView()
        {
            uploadFileViewModel = new UploadFileViewModel();
            DataContext = uploadFileViewModel;
            InitializeComponent();

            UploadFileViewModel.onNewFile += CloseWindow;
        }
        
        public void setFile(string fileName)
        {
            uploadFileViewModel.setFile.Execute(fileName);
        }

        public void CloseWindow(Models.File file)
        {
            this.Close();
        }
    }
}
