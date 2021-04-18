// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.IO;

namespace ModuleThreeFirstTaskConsole
{
    /// <summary>
    /// Main class. Start point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Console args.</param>
        public static void Main(string[] args)
        {
            var fs = new FileSystemVisitor(new DirectoryInfo(@"F:\Учёба, прога и т.п"), f => true);
            fs.SearchEnded += (sender, e) => Console.WriteLine($"Searching in {e.FullName} completed.");
            fs.SearchStarted += (sender, e) => Console.WriteLine($"Searching in {e.FullName} started.");
            foreach (var name in fs.Search())
            {
                Console.WriteLine(name);
            }
        }
    }
}
