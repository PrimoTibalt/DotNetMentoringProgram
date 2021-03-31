// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WeekFirstConsoleApp
{
    using System;
    using ModuleOneSecondTaskLibrary;

    /// <summary>
    /// Program runner.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Console attributes.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine(GreatingBuilder.Build());
            Console.Write("Click any button to exit program...");
            Console.ReadKey();
        }
    }
}
