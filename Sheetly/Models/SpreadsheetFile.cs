using Microsoft.VisualBasic.FileIO;
using Sheetly.lib;
using Sheetly.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
// Note: EPPlus used, which has a small fee for commercial use

namespace Sheetly.Models
{
    public partial class SpreadsheetFile : INotifyPropertyChanged
    {

        #region Commands
        public Command RemoveFromListCommand { get; set; }
        public static Action<SpreadsheetFile> onRemoveFromList;

        public Command EditFileCommand { get; set; }
        public static Action<SpreadsheetFile> onEditFile;
        
        public Command SetNextFileOperation { get; set; }
        #endregion

        #region Properties
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
            set {
                rows = value;
                this.RowCount = value.Count;
            }
        }

        private int rowCount;
        public int RowCount
        {
            get => rowCount;
            set { rowCount = value; }
        }

        private List<string> headers;
        public List<string> Headers
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
        public SpreadsheetFile(string filePath)
        {
            try
            {
                this.filePath = filePath;
                extension = GetExtension(filePath);
                name = GetFileName(filePath);
                delimiter = ",";
                ReadRows();
                Connection = new FileConnection();
                RemoveFromListCommand = new Command(RemoveFromList);
                EditFileCommand = new Command(EditFile);
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

        #region Command Methods
        
        public void RemoveFromList(object sender)
        {
            onRemoveFromList(this);
        }

        public void EditFile(object sender)
        {
            onEditFile(this);
        }

        #endregion

        #region Misc. Methods
        public void ReadRows()
        {
            switch (extension)
            {
                case ValidSpreadsheetExtensions.csv:
                case ValidSpreadsheetExtensions.txt:
                    ReadCSVIntoFile();
                    break;
                case ValidSpreadsheetExtensions.xlsx:
                case ValidSpreadsheetExtensions.xls:
                    ReadExcelIntoFile();
                    break;
                case ValidSpreadsheetExtensions.ods:
                case ValidSpreadsheetExtensions.ots:
                    ReadOdsIntoFile(filePath);
                    break;
                case ValidSpreadsheetExtensions.numbers:
                    break;
            }
        }

        public void SetOperation(object sender)
        {
            connection.Operation = (AllowedOperations)Enum.Parse(typeof(AllowedOperations), (string)sender);
            Debug.Print(connection.Operation.ToString());
        }

        public ValidSpreadsheetExtensions GetExtension(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).Split('.')[1];
            if (!(Enum.IsDefined(typeof(ValidSpreadsheetExtensions), fileExtension)))
                throw new ArgumentException(String.Format("{0} is not a correct spreadsheet format", fileExtension));
            else
                return (ValidSpreadsheetExtensions)Enum.Parse(typeof(ValidSpreadsheetExtensions), fileExtension);
        }


        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }
        #endregion
    }
}
