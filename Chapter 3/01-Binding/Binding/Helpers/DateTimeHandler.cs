using System;

namespace Binding.Helpers
{
    public static class DateTimeHandler
    {
        public static string ConvertToShortDate(DateTime dateTime)
        {
            return dateTime.ToShortDateString();
        }
    }

}
