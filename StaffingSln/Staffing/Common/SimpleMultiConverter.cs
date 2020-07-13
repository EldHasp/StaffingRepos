using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Common
{
    public class SimpleMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values.ToArray();

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
