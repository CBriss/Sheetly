using System;
using System.Text;
using OfficeOpenXml;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.IO;

namespace Sheetly.lib
{
    class FileReader
    {
        protected void ReadExcel(string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // NOTE: Below is example code

                //Get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int col = 2; //Column 2 is the item description
                for (int row = 2; row < 5; row++)
                    Console.WriteLine("\tCell({0},{1}).Value={2}", row, col, worksheet.Cells[row, col].Value);

                //Output the formula from row 3 in A1 and R1C1 format
                Console.WriteLine("\tCell({0},{1}).Formula={2}", 3, 5, worksheet.Cells[3, 5].Formula);
                Console.WriteLine("\tCell({0},{1}).FormulaR1C1={2}", 3, 5, worksheet.Cells[3, 5].FormulaR1C1);
            }
        }

        public void ReadOds(string filePath)
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

        public void ReadNumbers(string filePath)
        {
            // TODO
        }
    }
}
