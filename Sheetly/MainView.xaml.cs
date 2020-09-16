﻿using Sheetly.Models;
using Sheetly.ViewModels;
using Sheetly.Views;
using System.Windows;

namespace Sheetly
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainView : Window
    {
        FileViewModel fileViewModel;

        public MainView()
        {
            fileViewModel = new FileViewModel();
            DataContext = fileViewModel;
            InitializeComponent();

            UploadFileViewModel.onNewFile += AddNewFile;
        }

        private void NewFileWindow(object sender, RoutedEventArgs e)
        {
            UploadFileView newWidow = new UploadFileView();
            newWidow.Show();
        }

        public void AddNewFile(File newFile)
        {
            fileViewModel.AddFile(newFile);
        }
    }
}
