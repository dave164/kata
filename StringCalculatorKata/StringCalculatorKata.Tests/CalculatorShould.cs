using System;
using FluentAssertions;
using NUnit.Framework;
using StringCalculatorKata.Logic;

namespace StringCalculatorKata.Tests
{
    [TestFixture]
    public class CalculatorShould
    {
        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        [TestCase("1,2,3,4,5", 15)]
        [TestCase("1\n,2,3", 6)]
        public void ReturnSumOfString(string input, int expected)
        {
            var result = new StringCalculator().Add(input);
            result.Should().Be(expected);
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//,\n1,2", 3)]
        [TestCase("//@\n1@2", 3)]
        [TestCase("//;\n1", 1)]
        [TestCase("//;\n", 0)]
        public void SupportDelimeterThatIsntComma(string input, int expected)
        {
            var result = new StringCalculator().Add(input);
            result.Should().Be(expected);
        }

        [TestCase("//;;;;;;\n1;;;;;;2", 3)]
        public void SuportDelimiterOfAnyLength(string input, int expected)
        {
            var result = new StringCalculator().Add(input);
            result.Should().Be(expected);
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[**][%%]\n1**2%%3", 6)]
        public void SupportMultipleDelimiters(string input, int expected)
        {
            var result = new StringCalculator().Add(input);
            result.Should().Be(expected);
        }

        [TestCase("-1", "Numbers [ -1 ] was negative. This is not allowed.")]
        [TestCase("1,-2", "Numbers [ -2 ] was negative. This is not allowed.")]
        [TestCase("1,2,3,-4,5", "Numbers [ -4 ] was negative. This is not allowed.")]
        [TestCase("1\n,2,-3", "Numbers [ -3 ] was negative. This is not allowed.")]
        public void ThrowNegativesNotAllowedExceptionForNegativeNumber(string input, string expectedMessage)
        {
            Action action = () => new StringCalculator().Add(input);
            action.ShouldThrow<NegativesNotAllowedException>().WithMessage(expectedMessage);
        }

        [TestCase("-1,-2", "Numbers [ -1,-2 ] was negative. This is not allowed.")]
        public void ThrowNegativesNotAllowedExceptionContainingMessageForMultipleNegatives(string input, string expectedMessage)
        {
            Action action = () => new StringCalculator().Add(input);
            action.ShouldThrow<NegativesNotAllowedException>().WithMessage(expectedMessage);
        }

        [TestCase("2,1000", 1002)]
        [TestCase("2,1001", 2)]
        public void IgnoreNumbersGreaterThan1000(string input, int expected)
        {
            var result = new StringCalculator().Add(input);
            result.Should().Be(expected);
        }
    }
}
