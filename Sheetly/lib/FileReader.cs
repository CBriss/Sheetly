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

using File = Sheetly.Models.File;

namespace Sheetly.lib
{
    class FileReader
    {
        public static void ReadCSVIntoFile(File file, string filePath, string delimiter)
        {
            List<List<string>> rows = new List<List<string>>();
            
            // If quotes, add the following line
            // TextFieldParser.HasFieldsEnclosedInQuotes = true;
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.SetDelimiters(delimiter);
                while (!parser.EndOfData)
                {
                    List<string> fields = parser.ReadFields().ToList<string>();
                    rows.Add(fields);
                }
            }

            file.Rows = rows.Skip(1).ToList();
            file.Headers = rows[0];
        }
        
        public static void ReadExcelIntoFile(string filePath)
        {
            List<List<string>> rows = new List<List<string>>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                
                var start = worksheet.Dimension.Start;
                var end = worksheet.Dimension.End;
                List<string> row = new List<string>();
                for (int rowNum = start.Row; rowNum <= end.Row; rowNum++)
                {
                    row = new List<string>();
                    for (int col = start.Column; col <= end.Column; col++)
                    {
                        row.Add(worksheet.Cells[rowNum, col].Text);
                    }
                    // NOTE: Here, row is passed by address
                    //        So changing row changes the entire rows list
                    rows.Add(row);
                }
            }
            //return rows;
        }

        public static void ReadOdsIntoFile(string filePath)
        {
            // NOTE: Below is example code

            var sb = new StringBuilder();
            using (var doc = new TextDocument())
            {
                doc.Load(filePath);

                //The header and footer are in the DocumentStyles part. Grab the XML of this part
                XElement stylesPart = XElement.Parse(doc.DocumentStyles.Styles.OuterXml);
                //Take all headers and footers text, concatenated with return carriage
                string stylesText = string.Join("\r\n", stylesPart.Descendants().Where(x => x.Name.LocalName == "header" || x.Name.LocalName == "footer").Select(y => y.Value));

                //Main content
                var mainPart = doc.Content.Cast<IContent>();
                var mainText = String.Join("\r\n", mainPart.Select(x => x.Node.InnerText));

                //Append both text variables
                sb.Append(stylesText + "\r\n");
                sb.Append(mainText);
            }
        }

        public static void ReadNumbersIntoFile(string filePath)
        {
            // TODO
        }
    }
}
