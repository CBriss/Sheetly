using Sheetly.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

// Note: EPPlus used, which has a small fee for commercial use

namespace Sheetly
{
    public class File : INotifyPropertyChanged
    {
        public enum SpreadsheetExtensions
        {
            csv,
            txt,
            xls,
            xlsx,
            numbers,
            ods,
            ots
        }
        public File(string filePath) {
            string fileExtension = Path.GetExtension(filePath).Split('.')[1];
            if(!(Enum.IsDefined(typeof(SpreadsheetExtensions), fileExtension))) {
                throw new ArgumentException(String.Format("{0} is not a correct spreadsheet format", fileExtension));
            }

            this.filePath = filePath;
            extension = (SpreadsheetExtensions)Enum.Parse(typeof(SpreadsheetExtensions), fileExtension);
            delimiter = ",";
            ReadRows();
        }

        private string filePath;
        public string FilePath {
            get { return filePath; }
            set { filePath = value; OnPropertyChange(filePath); }
        }

        public string FileName
        {
            get { return filePath.Split('/')[^1]; }
        }

        private SpreadsheetExtensions extension;
        public SpreadsheetExtensions Extension
        {
            get { return extension; }
            set { extension = value; OnPropertyChange(extension.ToString()); }
        }

        private List<Array> rows;
        public List<Array> Rows
        {
            get { return rows;  }
            set { rows = value; }
        }

        private int rowCount;
        public int RowCount
        {
            get { return rowCount; }
            set { rowCount = value; }
        }

        private List<String> headers;
        public List<String> Headers
        {
            get { return headers; }
            set { headers = value;  }
        }

        private string name;
        public string Name {
            get { return name; }
            set { name = value; } 
        }

        private string delimiter;
        public string Delimiter
        {
            get { return delimiter; }
            set { delimiter = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReadRows()
        {
            switch (extension)
            {
                case SpreadsheetExtensions.csv:
                    Console.WriteLine("csv");
                    rows = FileReader.ReadCSV(filePath, delimiter);
                    break;
                case SpreadsheetExtensions.txt:
                    Console.WriteLine("txt");
                    rows = FileReader.ReadCSV(filePath, delimiter);
                    break;
                case SpreadsheetExtensions.xls:
                    rows = FileReader.ReadExcel(filePath);
                    Console.WriteLine("xls");
                    break;
                case SpreadsheetExtensions.xlsx:
                    rows = FileReader.ReadExcel(filePath);
                    Console.WriteLine("xlsx");
                    break;
                case SpreadsheetExtensions.numbers:
                    Console.WriteLine("numbers");
                    break;
                case SpreadsheetExtensions.ods:
                    Console.WriteLine("ods");
                    FileReader.ReadOds(filePath);
                    break;
                case SpreadsheetExtensions.ots:
                    Console.WriteLine("ots");
                    FileReader.ReadOds(filePath);
                    break;
            }
            rowCount = rows.Count;
            headers = new List<string>((IEnumerable<string>)rows[0]);
        }
    }
}
