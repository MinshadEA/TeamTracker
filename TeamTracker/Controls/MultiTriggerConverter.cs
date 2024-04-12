using System;
using System.Globalization;

namespace TeamTracker.Controls
{
    public class MultiTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;

            if ((int)value > 0) return true;
            else return false;   // Input is empty
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

