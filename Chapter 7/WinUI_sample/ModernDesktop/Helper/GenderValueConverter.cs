using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using ModernDesktop.Model;

namespace ModernDesktop.Helper
{
    public class GenderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var v = value?.ToString();
            return !string.IsNullOrWhiteSpace(v) ? Enum.Parse(typeof(Gender), v) : Gender.Other;
        }
    }
}
