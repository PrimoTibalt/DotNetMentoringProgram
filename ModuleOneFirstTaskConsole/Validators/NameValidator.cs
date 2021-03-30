// <copyright file="NameValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WeekFirstConsoleApp.Validators
{
    /// <summary>
    /// Validates names of persons.
    /// </summary>
    public class NameValidator
    {
        /// <summary>
        /// Checks is name starts with upper case symbol.
        /// </summary>
        /// <param name="text">Name of person.</param>
        /// <returns>Is valid name or not.</returns>
        public bool Validate(string text)
        {
            return char.IsUpper(text[0]);
        }
    }
}
