using System;
using System.Text;
using OfficeOpenXml;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using Sheetly.Models;

namespace Sheetly.lib
{
    class FileWriter
    {
        public static void WriteCSV(SpreadsheetFile file, string filePath, string delimiter)
        {
            StringBuilder sbOutput = new StringBuilder();
            for (int i = 0; i < file.RowCount; i++)
                sbOutput.AppendLine(string.Join(delimiter, file.Rows[i]));
            File.WriteAllText(filePath, sbOutput.ToString());
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
