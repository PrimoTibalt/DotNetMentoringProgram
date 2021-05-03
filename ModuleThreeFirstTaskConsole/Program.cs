// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
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
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                { @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\files\home.txt", new MockFileData("Testing is meh.") },
                { @"c:\files\jQuery.js", new MockFileData("some js") },
                { @"c:\files\sun\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\files\sun\aaawifihsdifhish.txt", new MockFileData("Testing is meh.") },
                { @"c:\files\zangetsu\motherland.js", new MockFileData("some js") },
                { @"c:\files\zangetsu\moon\neiborhood.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\files\stand\here\comrad\sunday.txt", new MockFileData("Testing is meh.") },
                { @"c:\files\mimik\spider\man\tongue\control\panel\MilesMorales.js", new MockFileData("some js") },
                { @"c:\files\mimik\spider\man\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\files\mimik\spider\man\tongue\control\Gokuden.txt", new MockFileData("Testing is meh.") },
                { @"c:\files\mimik\spider\man\tongue\rainbow.js", new MockFileData("some js") },
                { @"c:\files\mimik\spider\man\tongue\colors\pink.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\files\mirror\clone.txt", new MockFileData("Testing is meh.") },
                { @"c:\dnd\mage.js", new MockFileData("some js") },
                { @"c:\dnd\warrior.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\dnd\bard.txt", new MockFileData("Testing is meh.") },
                { @"c:\dnd\healer.js", new MockFileData("some js") },
                { @"c:\demo\darksidewhy.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\fate\astolfo.txt", new MockFileData("Testing is meh.") },
                { @"c:\fate\Emia.js", new MockFileData("some js") },
                { @"c:\fate\Shiro.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\fate\Archer.txt", new MockFileData("Testing is meh.") },
                { @"c:\fate\Saber.js", new MockFileData("some js") },
                { @"c:\fate\stay\night\unlimited\blade\works\heavens\feel\apocrif\prototype\tsukihime\moon\princess\arkveit.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
            });

            var fs = new FileSystemVisitor(fileSystem, f => true, @"c:\");
            fs.SearchEnded += (sender, e) => Console.WriteLine($"Searching in {e.FullName} completed.");
            fs.SearchStarted += (sender, e) => Console.WriteLine($"Searching in {e.FullName} started.");
            fs.FilteredDirectoryFound += (sender, e) => e.Exclude = false;
            var task = Task.Run(fs.Search);
            await foreach (var name in task.GetAwaiter().GetResult())
            {
                Console.WriteLine(name);
            }
        }
    }
}
