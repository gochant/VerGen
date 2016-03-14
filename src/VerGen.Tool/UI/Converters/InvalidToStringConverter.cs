using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VerGen.Tool.UI.Converters
{
    public sealed class InvalidToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanValue = (bool)value;
            return booleanValue ? "-" : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("The converter can only be used OneWay.");
        }
    }
}