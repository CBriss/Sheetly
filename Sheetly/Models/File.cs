using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

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

            // Set File Path
            this.filePath = filePath;
            // Determine file type
            extension = (SpreadsheetExtensions)Enum.Parse(typeof(SpreadsheetExtensions), fileExtension);
        }

        private string filePath;
        public string FilePath {
            get { return filePath; }
            set { filePath = value; OnPropertyChange(filePath); }
        }

        public string FileName
        {
            get { return filePath.Split('/')[filePath.Split('/').Length-1]; }
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
        private string name;
        public string Name {
            get { return name; }
            set { name = value; } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
