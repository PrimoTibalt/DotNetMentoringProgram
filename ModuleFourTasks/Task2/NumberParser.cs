using System;
using System.Collections.Generic;

namespace Task2
{
    /// <summary>
    /// Class to parse string to int.
    /// </summary>
    public class NumberParser : INumberParser
    {
        private readonly Dictionary<char, int> _charToInt = new Dictionary<char, int>
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
        };

        /// <summary>
        /// Parses string to integer.
        /// </summary>
        /// <param name="stringValue">Number in string object.</param>
        /// <returns>Integer from stringValue.</returns>
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            var formatedInputSign = FormatInput(stringValue);
            var formatedInput = formatedInputSign.Item1;
            var sign = formatedInputSign.Item2;

            var result = GetNumberFromFormatedInput(formatedInput, sign);

            return result;
        }

        private (string, int) FormatInput(string input)
        {
            input = input.Trim();
            if (input.Length < 1 || input.IndexOf('-') > 0 || input.IndexOf('+') > 0 || input.IndexOf(' ') > -1)
            {
                throw new FormatException();
            }

            return new ValueTuple<string, int>(input.Replace("-", string.Empty).Replace("+", string.Empty), input[0] == '-' ? -1 : 1);
        }

        private int GetNumberFromFormatedInput(string input, int sign)
        {
            var result = 0;
            var count = input.Length;
            foreach (var charecter in input)
            {
                if (!_charToInt.ContainsKey(charecter))
                {
                    throw new FormatException();
                }

                var multiplier = 1;
                for (var times = --count; times > 0; times--)
                {
                    multiplier *= 10;
                }

                checked
                {
                    result += sign * _charToInt[charecter] * multiplier;
                }
            }

            return result;
        }
    }
}
