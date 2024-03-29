﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Sheetly.Models;
using Sheetly.Views;
using Sheetly.lib;

namespace Sheetly.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public static Action<string> onNewFileUploaded;

        #region Commands
        public Command findNewFile { get; set; }
        public Command processFiles { get; set; }
        #endregion
        
        #region Properties
        public ObservableCollection<SpreadsheetFile> _fileList;
        public ObservableCollection<SpreadsheetFile> FileList
        {
            get { return _fileList; }
            set
            {
                _fileList = value;
                OnPropertyChanged("FileList");
            }
        }

        public SpreadsheetFile _outputFile;
        public SpreadsheetFile OutputFile
        {
            get { return _outputFile; }
            set
            {
                _outputFile = value;
                OnPropertyChanged("OutputFile");
            }
        }
        #endregion

        #region Base Methods
        public MainViewModel() {
            // Set up empty file list
            _fileList = new ObservableCollection<SpreadsheetFile>();

            // Add to actions
            SpreadsheetFile.onRemoveFromList += RemoveFile;

            // Set up commands
            findNewFile = new Command(UploadNewFileFromFinderCommand);
            processFiles = new Command(ProcessFiles);
        }
        #endregion

        #region Other Methods
        public void AddFile(SpreadsheetFile newFile)
        {
            try
            {
                if(FileList.Count > 0)
                {
                    SpreadsheetFile lastFile = FileList.Last();
                    lastFile.Connection.NextFile = newFile;
                }   

                FileList.Add(newFile);
                System.Diagnostics.Debug.WriteLine("# of Files: " + FileList.Count);
                OnPropertyChanged("FileList");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Exception");
            }   
        }

        public void RemoveFile(object sender)
        {
            FileList.Remove((SpreadsheetFile) sender);
            System.Diagnostics.Debug.WriteLine("# of Files: " + FileList.Count);
            OnPropertyChanged("FileList");
        }

        public void OpenEditFileView(SpreadsheetFile file)
        {
            EditFileView editFileView = new EditFileView();
            editFileView.WillEventuallyBeSetFile(file);
            editFileView.Show();
        }

        private void UploadNewFileFromFinderCommand(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Accepted files |*.txt;*.csv;*.xls;*.xlsx;*.number;*.odt";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    onNewFileUploaded(fileDialog.FileName);
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message); ;
                }

            }
        }

        private void ProcessFiles(object sender)
        {
            if (FileList.Count <= 0)
                return;
            else if (FileList.Count == 1)
            {
                OutputFile = FileList[0];
                return;
            }
                
            OutputFile = FileList[0];
            SpreadsheetFile nextFile = FileList[0].Connection.NextFile;
            AllowedOperations nextOperation = FileList[0].Connection.Operation;
            
            while(nextFile != null)
            {
                switch (nextOperation)
                {
                    case AllowedOperations.Add:
                        OutputFile.Rows = FileOperations.Add(OutputFile.Rows, nextFile.Rows);
                        break;
                    case AllowedOperations.Subtract:
                        OutputFile.Rows = FileOperations.Subtract(
                            OutputFile.Rows,
                            OutputFile.Headers.IndexOf(OutputFile.IndexColumn),
                            nextFile.Rows,
                            nextFile.Headers.IndexOf(nextFile.IndexColumn)
                            );
                        break;
                    case AllowedOperations.Combine:
                        OutputFile.Rows = FileOperations.Combine(
                            OutputFile.Rows,
                            OutputFile.Headers.IndexOf(OutputFile.IndexColumn),
                            nextFile.Rows,
                            nextFile.Headers.IndexOf(nextFile.IndexColumn)
                            );
                        break;
                    case AllowedOperations.CountCommon:
                        int outputNumber = FileOperations.CountCommon(
                            OutputFile.Rows,
                            OutputFile.Headers.IndexOf(OutputFile.IndexColumn),
                            nextFile.Rows,
                            nextFile.Headers.IndexOf(nextFile.IndexColumn)
                            );
                        Debug.Print("common people are: " + outputNumber.ToString());
                        break;
                    case AllowedOperations.Unique:
                        OutputFile.Rows = FileOperations.Unique(
                            OutputFile.Rows,
                            OutputFile.Headers.IndexOf(OutputFile.IndexColumn)
                            );
                        break;
                default:
                        break;
                }
                nextOperation = nextFile.Connection.Operation;
                nextFile = nextFile.Connection.NextFile;
            }

            SaveOutputFile();
        }

        private void SaveOutputFile()
        {
            OutputFile.FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            OutputFile.Name = "TestOutputFile";
            FileWriter.WriteCSV(OutputFile);
        }
        #endregion
    }
}
