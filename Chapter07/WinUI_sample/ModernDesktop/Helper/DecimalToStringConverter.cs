using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace ModernDesktop.Helper
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = "0";
            if (value is decimal v) result = v.ToString();
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            decimal result = 0;
            var v = value?.ToString();
            if (!string.IsNullOrEmpty(v))
            {
                if(decimal.TryParse(v, out var d)) result = d;
            }

            return result;
        }
    }
}
