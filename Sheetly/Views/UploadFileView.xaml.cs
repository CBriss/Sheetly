using Sheetly.ViewModels;
using Sheetly.Models;

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

        public void CloseWindow(File file)
        {
            this.Close();
        }
    }
}
