using Microsoft.Win32;
using Sheetly.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sheetly
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        FileViewModel fileViewModel;

        public MainMenu()
        {
            fileViewModel = new FileViewModel();

            // The DataContext serves as the starting point of Binding Paths
            DataContext = fileViewModel;

            InitializeComponent();
        }

        private void UploadNewFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                fileViewModel.AddFile(fileDialog.FileName);
            }
        }
    }
}
