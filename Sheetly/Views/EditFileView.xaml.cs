﻿using Sheetly.ViewModels;
using Sheetly.Models;
using System.Windows;
using System.IO;

namespace Sheetly.Views
{
    /// <summary>
    /// Interaction logic for NewFile.xaml
    /// </summary>
    public partial class EditFileView : Window
    {
        EditFileViewModel editFileViewModel;

        #region Base Methods
        public EditFileView()
        {
            editFileViewModel = new EditFileViewModel();
            DataContext = editFileViewModel;
            InitializeComponent();

            EditFileViewModel.onNewFile += CloseWindow;
        }
        #endregion

        #region Other Methods
        public void setFile(string fileName)
        {
            editFileViewModel.setFile.Execute(fileName);
        }

        public void WillEventuallyBeSetFile(SpreadsheetFile file)
        {
            editFileViewModel.NewFile = file;
        }

        public void CloseWindow(SpreadsheetFile file)
        {
            this.Close();
        }
        #endregion
    }
}
