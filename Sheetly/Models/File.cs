using Sheetly.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

// Note: EPPlus used, which has a small fee for commercial use

namespace Sheetly.Models
{
    public class File : INotifyPropertyChanged
    {
        #region Properties

        private string filePath;
        public string FilePath
        {
            get =>  filePath;
            set { filePath = value; OnPropertyChange(filePath); }
        }

        public string FileName
        {
            get { return filePath.Split('/')[^1]; }
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
        #endregion

        #region Base Methods
        public File(string filePath) {
            string fileExtension = Path.GetExtension(filePath).Split('.')[1];
            if(!(Enum.IsDefined(typeof(ValidSpreadsheetExtensions), fileExtension))) {
                throw new ArgumentException(String.Format("{0} is not a correct spreadsheet format", fileExtension));
            }

            this.filePath = filePath;
            extension = (ValidSpreadsheetExtensions)Enum.Parse(typeof(ValidSpreadsheetExtensions), fileExtension);
            delimiter = ",";
            ReadRows();
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
            headers = new List<string>((IEnumerable<string>)rows[0]);
        }
    }
}
