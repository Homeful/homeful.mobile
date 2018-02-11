using System;
using System.Globalization;
using Xamarin.Forms;

namespace Homeful.forms.Converters
{
    public class BooleanToVisibleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
