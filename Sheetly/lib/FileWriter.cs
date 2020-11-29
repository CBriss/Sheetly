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

namespace Sheetly.lib
{
    class FileWriter
    {
        public static void WriteCSV(string filePath, string delimiter)
        {
            string strFilePath = @"D:\New folder\Data.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();
            for (int i = 0; i & amp; lt; ilength; i++)
                sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));

            // Create and write the csv file
            File.WriteAllText(strFilePath, sbOutput.ToString());

            // To append more lines to the csv file
            File.AppendAllText(strFilePath, sbOutput.ToString());
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
