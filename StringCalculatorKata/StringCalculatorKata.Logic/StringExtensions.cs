using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorKata.Logic
{
    public static class StringExtensions
    {
        public static string EnumerableToCsv(this IEnumerable<int> values)
        {
            string returnValue = string.Empty;
            foreach (int value in values)
            {
                returnValue += $"{value},";
            }

            return returnValue.Substring(0, returnValue.Length - 1);
        }

        public static IEnumerable<string> SplitOnDelimiter(this string value, IEnumerable<string> delimiters)
        {
            return value.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}