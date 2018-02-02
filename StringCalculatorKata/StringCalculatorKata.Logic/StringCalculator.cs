using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorKata.Logic
{
    public class StringCalculator
    {
        private const string regexPattern = "//(.*)\n";
        private const string secondRegexPattern = "\\[([^\\[\\]]+)\\]";

        public int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var delimitors = GetDelimitor(numbers);

            var numbersAsCsv = GetStringWithoutDelimitorSpecification(numbers);

            var numberList = numbersAsCsv.SplitOnDelimiter(delimitors);

            return SumOfNumbers(numberList);
        }

        private static int SumOfNumbers(IEnumerable<string> numberList)
        {
            var negatives = new List<int>();
            var current = 0;
            foreach (var stringNumber in numberList)
            {
                var number = int.Parse(stringNumber);
                if (number < 0)
                    negatives.Add(number);
                if (number <= 1000)
                    current += number;
            }

            if (negatives.Any())
                throw new NegativesNotAllowedException(
                    $"Numbers [ {negatives.EnumerableToCsv()} ] was negative. This is not allowed.");
            return current;
        }

        private IEnumerable<string> GetDelimitor(string numbers)
        {
            List<string> delimitor = new List<string> { "," };
            var matches = Regex.Matches(numbers, secondRegexPattern);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        delimitor.Add(match.Groups[1].Value);
                    }
                }
            }
            else
            {
                var match = Regex.Match(numbers, regexPattern);
                if (match.Success)
                    delimitor.Add(match.Groups[1].Value);
            }

            return delimitor;
        }

        private string GetStringWithoutDelimitorSpecification(string numbers)
        {
            string returnValue = numbers;
            var match = Regex.Match(numbers, regexPattern);
            if (match.Success)
            {
                returnValue = numbers.Replace(match.Value, string.Empty);
            }
            return returnValue;
        }
    }
}
