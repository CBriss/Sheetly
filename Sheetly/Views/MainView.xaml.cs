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
            InitializeComponent();
        }

        private void NewFileWindow(object sender, RoutedEventArgs e)
        {
            UploadFileView newWidow = new UploadFileView();
            newWidow.Show();
        }
    }
}
