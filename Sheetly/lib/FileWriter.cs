using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using Sheetly.Models;

namespace Sheetly.lib
{
    class FileWriter
    {
        public static void WriteCSV(SpreadsheetFile file)
        {
            StringBuilder outputString = new StringBuilder();
            for (int i = 0; i < file.RowCount; i++)
                outputString.AppendLine(string.Join(file.Delimiter, file.Rows[i]));
            System.IO.File.WriteAllText(Path.Combine(file.FilePath, file.Name), outputString.ToString());
        }

        public static void WriteExcel(string filePath, string delimiter)
        {
        }

        public static void WriteOds(string filePath, string delimiter)
        {
        }

        public static void WriteNumbers(string filePath, string delimiter)
        {
        }
    }
}
