using System;
using Windows.UI.Xaml.Data;
using UWP_Desktop.Models;

namespace UWP_Desktop.Converter
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
