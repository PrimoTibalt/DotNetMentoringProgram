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
            var greatingBuilder = new GreatingBuilder();
            Console.WriteLine(greatingBuilder.Build());
            Console.ReadKey();
        }
    }
}
