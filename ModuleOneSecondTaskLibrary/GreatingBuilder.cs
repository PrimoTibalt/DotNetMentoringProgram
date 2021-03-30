// <copyright file="GreatingBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace ModuleOneSecondTaskLibrary
{
    /// <summary>
    /// Builds greating text.
    /// </summary>
    public class GreatingBuilder
    {
        /// <summary>
        /// Returns greating in format:
        /// «{current_time} Hello, {name}!».
        /// </summary>
        /// <returns>Greating text.</returns>
        public string Build()
        {
            var args = Environment.GetCommandLineArgs();
            var name = args?.Length > 1 ? args[1] : Environment.UserName;
            return $"{DateTime.Now.TimeOfDay} Hello, {name}!";
        }
    }
}
