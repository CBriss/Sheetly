using System;
using System.Globalization;

using Sheetly.Models;

namespace Sheetly.ValueConverters
{
    /// <summary>
    /// Converts a binding into a <see cref="Models.File"/>
    /// </summary>
    public class FileValueConverter : BaseValueConverter<FileValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SpreadsheetFile)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
