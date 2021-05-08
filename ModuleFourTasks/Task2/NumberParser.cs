using System;
using System.Collections.Generic;
using System.Linq;

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

        private int _count = 0;
        private int _result = 0;
        private int _resultBackup = 0;
        private int _signMultiplier = 0;

        /// <summary>
        /// Parses string to integer.
        /// </summary>
        /// <param name="stringValue">Number in string object.</param>
        /// <returns>Integer from stringValue.</returns>
        public int Parse(string stringValue)
        {
            _resultBackup = 0;
            if (stringValue is null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            stringValue = FormatInput(stringValue);
            _count = stringValue.Length;
            try
            {
                stringValue.ToList().ForEach(num => ParseToResult(num));
            }
            finally
            {
                _resultBackup = _result;
                _result = 0;
                _count = 0;
            }

            return _resultBackup;
        }

        private void ParseToResult(char ch)
        {
            if (!char.IsNumber(ch))
            {
                throw new FormatException();
            }

            var multiplier = 1;
            for (var times = --_count; times > 0; times--)
            {
                multiplier *= 10;
            }

            checked
            {
                _result += _signMultiplier * _charToInt[ch] * multiplier;
            }
        }

        private string FormatInput(string input)
        {
            input = input.Trim();
            if (input.Length < 1 || input.IndexOf('-') > 0 || input.IndexOf('+') > 0 || input.IndexOf(' ') > -1)
            {
                throw new FormatException();
            }

            _signMultiplier = input[0] == '-' ? -1 : 1;
            return input.Replace("-", string.Empty).Replace("+", string.Empty);
        }
    }
}
