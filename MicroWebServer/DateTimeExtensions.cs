using System;
using Microsoft.SPOT;

namespace MicroWebServer
{
    public static class DateTimeExtensions
    {
        public static string ToLastModifiedString(this DateTime date)
        {
            return date.ToString("ddd, dd MMM yyyy hh:mm:ss ") + "GMT";
        }
    }
}
