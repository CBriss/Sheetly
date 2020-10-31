﻿using Microsoft.VisualBasic.FileIO;
using Sheetly.lib;
using Sheetly.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

// Note: EPPlus used, which has a small fee for commercial use

namespace Sheetly.Models
{
    public class File : INotifyPropertyChanged
    {
        #region Properties

        public static Action<File> onRemoveFromList;
        public Command RemoveFromList { get; set; }
        public Command SetNextFileOperation { get; set; }

        private string filePath;
        public string FilePath
        {
            get =>  filePath;
            set { filePath = value; OnPropertyChange(filePath); }
        }

        private ValidSpreadsheetExtensions extension;
        public ValidSpreadsheetExtensions Extension
        {
            get => extension;
            set { extension = value; OnPropertyChange(extension.ToString()); }
        }

        private List<List<string>> rows;
        public List<List<string>> Rows
        {
            get => rows;
            set { rows = value; }
        }

        private int rowCount;
        public int RowCount
        {
            get => rowCount;
            set { rowCount = value; }
        }

        private List<String> headers;
        public List<String> Headers
        {
            get => headers;
            set { headers = value; }
        }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; }
        }

        private string delimiter;
        public string Delimiter
        {
            get => delimiter;
            set { delimiter = value; }
        }

        private string indexColumn;
        public string IndexColumn
        {
            get => indexColumn;
            set { indexColumn = value; }
        }

        private FileConnection connection;
        public FileConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        #endregion

        #region Base Methods
        public File(string filePath) {
            string fileExtension = Path.GetExtension(filePath).Split('.')[1];
            if(!(Enum.IsDefined(typeof(ValidSpreadsheetExtensions), fileExtension))) {
                throw new ArgumentException(String.Format("{0} is not a correct spreadsheet format", fileExtension));
            }
            try
            {
                this.filePath = filePath;
                this.name = "New File";
                extension = (ValidSpreadsheetExtensions)Enum.Parse(typeof(ValidSpreadsheetExtensions), fileExtension);
                delimiter = ",";
                ReadRows();
                Connection = new FileConnection();

                RemoveFromList = new Command(Remove);

                SetNextFileOperation = new Command(SetOperation);
            }
            catch (MalformedLineException e)
            {
                throw new ArgumentException(String.Format("malformed file in \n{0}", filePath));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void ReadRows()
        {
            switch (extension)
            {
                case ValidSpreadsheetExtensions.csv:
                    Console.WriteLine("csv");
                    rows = FileReader.ReadCSV(filePath, delimiter);
                    break;
                case ValidSpreadsheetExtensions.txt:
                    Console.WriteLine("txt");
                    rows = FileReader.ReadCSV(filePath, delimiter);
                    break;
                case ValidSpreadsheetExtensions.xls:
                    rows = FileReader.ReadExcel(filePath);
                    Console.WriteLine("xls");
                    break;
                case ValidSpreadsheetExtensions.xlsx:
                    rows = FileReader.ReadExcel(filePath);
                    Console.WriteLine("xlsx");
                    break;
                case ValidSpreadsheetExtensions.numbers:
                    Console.WriteLine("numbers");
                    break;
                case ValidSpreadsheetExtensions.ods:
                    Console.WriteLine("ods");
                    FileReader.ReadOds(filePath);
                    break;
                case ValidSpreadsheetExtensions.ots:
                    Console.WriteLine("ots");
                    FileReader.ReadOds(filePath);
                    break;
            }
            rowCount = rows.Count;
            Headers = new List<string>(rows[0]);
        }
        
        public void Remove(object sender)
        {
            onRemoveFromList(this);
        }

        public void SetOperation(object sender)
        {
            connection.Operation = (AllowedOperations)Enum.Parse(typeof(AllowedOperations), (string)sender);
            Debug.Print(connection.Operation.ToString());
        }

    }
}
