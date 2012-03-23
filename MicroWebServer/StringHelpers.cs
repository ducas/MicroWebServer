using System;
using Microsoft.SPOT;

namespace MicroWebServer
{
    public class StringHelpers
    {
        public static bool IsNullOrEmpty(string value)
        {
            return value == null || value == string.Empty;
        }

        public static string Replace(string item, string oldValue, string newValue)
        {
            if (IsNullOrEmpty(item)) return item;

            int previousIndex = 0, currentIndex = 0;

            var result = string.Empty;
            while ((currentIndex = item.IndexOf(oldValue, previousIndex)) > -1)
            {
                result += item.Substring(previousIndex, currentIndex - previousIndex) + newValue;
                previousIndex = currentIndex + oldValue.Length;
            }
            result += item.Substring(previousIndex);

            return result;
        }
    }
}
