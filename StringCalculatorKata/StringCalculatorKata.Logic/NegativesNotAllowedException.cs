using System;

namespace StringCalculatorKata.Logic
{
    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException(string message)
            : base(message)
        {
        }
    }
}