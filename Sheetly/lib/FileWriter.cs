using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using Sheetly.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Sheetly.lib
{
    class FileWriter
    {
        public static void WriteCSV(SpreadsheetFile file)
        {
            StringBuilder outputString = new StringBuilder();
            outputString.AppendLine(string.Join(file.Delimiter, file.Headers));
            for (int i = 0; i < file.RowCount; i++)
                outputString.AppendLine(string.Join(file.Delimiter, file.Rows[i]));
            System.IO.File.WriteAllText(Path.Combine(file.FilePath, file.Name + ".csv"), outputString.ToString());
        }

        public static void WriteExcel(SpreadsheetFile file)
        {
            // Creating an instance 
            // of ExcelPackage
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            // name of the sheet 
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            // setting the properties 
            // of the work sheet  
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            // Setting the properties 
            // of the first row 
            //workSheet.Row(1).Height = 20;
            //workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //workSheet.Row(1).Style.Font.Bold = true;

            // Header of the Excel sheet 
            //workSheet.Cells[1, 1].Value = "S.No";
            //workSheet.Cells[1, 2].Value = "Id";
            //workSheet.Cells[1, 3].Value = "Name";
            int rowIndex = 1;
            int columnIndex = 1;
            foreach (var value in file.Headers)
            {    
                workSheet.Cells[rowIndex, columnIndex].Value = value;
                columnIndex++;
            }

            // Inserting the article data into excel 
            // sheet by using the for each loop 
            // As we have values to the first row  
            // we will start with second row 
            rowIndex = 1;

            foreach (var row in file.Rows)
            {
                columnIndex = 1;
                foreach (var value in row)
                {
                    workSheet.Cells[rowIndex, columnIndex].Value = value;
                    columnIndex++;
                }
                rowIndex++;
            }

            // By default, the column width is not  
            // set to auto fit for the content 
            // of the range, so we are using 
            // AutoFit() method here.  
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();

            // file name with .xlsx extension  
            string p_strPath = Path.Combine(file.FilePath, file.Name + ".xlsx");

            if (File.Exists(p_strPath))
                File.Delete(p_strPath);

            // Create excel file on physical disk  
            FileStream objFileStrm = File.Create(p_strPath);
            objFileStrm.Close();

            // Write content to excel file  
            File.WriteAllBytes(p_strPath, excel.GetAsByteArray());
            //Close Excel package 
            excel.Dispose();
            System.Diagnostics.Debug.WriteLine(p_strPath);
        }

        public static void WriteOds(string filePath, string delimiter)
        {
        }

        public static void WriteNumbers(string filePath, string delimiter)
        {
        }
    }
}
