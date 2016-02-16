using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using VerGen.Tool.Utilities;

namespace VerGen.Tool.UI.Converters
{
    public class SelectListItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return DataTypeHelper.GetDataTypeList()
                .Select((data, index) => new { data, index })
                .FirstOrDefault(d => d.data.Value == value?.ToString())?.index;
               // .FirstOrDefault(d => d.Value == value?.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
          //  var item = (SelectListItem)value;
           return DataTypeHelper.GetDataTypeList()[int.Parse(value?.ToString())];
          //  return item?.Value;
        }
    }
}