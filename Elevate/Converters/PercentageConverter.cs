using System.Globalization;
using Microsoft.Maui.Controls;

namespace Elevate.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue / 100.0;
            }
            if (value is int intValue)
            {
                return intValue / 100.0;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue * 100.0;
            }
            return 0;
        }
    }
}