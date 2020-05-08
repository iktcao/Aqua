using System;
using System.Windows.Data;

namespace Aqua.Helper
{
    public class DoubleToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string str = (string)value;
            double dbl;
            if (double.TryParse(str, out dbl))
            {
                return dbl;
            }
            else
            {
                return str;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
