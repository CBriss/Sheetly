using System.Text;
using System.IO;
using File = Sheetly.Models.File;

namespace Sheetly.lib
{
    class FileWriter
    {
        public static void WriteCSV(File file)
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
