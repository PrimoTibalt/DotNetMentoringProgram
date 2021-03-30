// <copyright file="ConsolePrinter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WeekFirstConsoleApp.Printers
{
    using System;
    using WeekFirstConsoleApp.Validators;

    /// <summary>
    /// Prints some text in CLI.
    /// </summary>
    public class ConsolePrinter
    {
        /// <summary>
        /// Prints greating with name from console attributes or if it wasn't provided with computer name.
        /// </summary>
        public static void PrintGreating()
        {
            var args = Environment.GetCommandLineArgs();
            var validator = new NameValidator();
            if (args?.Length > 1)
            {
                var name = args[1];
                if (validator.Validate(name))
                {
                    Console.WriteLine($"Hello, {name}!");
                }
                else
                {
                    Console.WriteLine("Name should start with upper case symbol. Try again...");
                }
            }
            else
            {
                Console.WriteLine($"Hello, {Environment.UserName}!");
            }

            Console.ReadKey();
        }
    }
}
