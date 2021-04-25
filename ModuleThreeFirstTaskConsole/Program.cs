// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Threading.Tasks;

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
            MainAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Runs async FileSystemVisitor.
        /// </summary>
        /// <returns></returns>
        public static async Task MainAsync()
        {
            var fs = new FileSystemVisitor(new DirectoryInfo(@"F:\Учёба, прога и т.п"), f => true);
            fs.SearchEnded += (sender, e) => Console.WriteLine($"Searching in {e.FullName} completed.");
            fs.SearchStarted += (sender, e) => Console.WriteLine($"Searching in {e.FullName} started.");
            fs.FilteredDirectoryFound += (sender, e) => e.Exclude = e.Info.FullName.Contains("Udemy") ? true : false;
            var task = Task.Run(fs.Search);
            await foreach (var name in task.GetAwaiter().GetResult())
            {
                Console.WriteLine(name);
            }
        }
    }
}
