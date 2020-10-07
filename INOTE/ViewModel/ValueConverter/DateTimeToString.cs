using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace INOTE.ViewModel.ValueConverter
{
    public class DateTimeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;

            if(dateTime != null)
            {
                return dateTime.ToString();
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateTimeString = value.ToString();

            if(dateTimeString != null)
            {
                return DateTime.Parse(dateTimeString);
            }

            return DateTime.Now;
        }
    }
}
