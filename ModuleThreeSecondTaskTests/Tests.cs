﻿using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;
using ModuleThreeFirstTaskConsole;
using Xunit;

namespace ModuleThreeSecondTaskTests
{
    /// <summary>
    /// Tests related to FileSystemVisitor.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Checkes do the visitor returns all files from provided filesystem.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task Method_AllFiles_Returned()
        {
            var paths = new List<string>
            {
                @"c:\myfile.txt",
                @"c:\demo\jQuery.js",
                @"c:\demo\image.gif",
                @"c:\files\home.txt",
                @"c:\files\jQuery.js",
                @"c:\files\sun\image.gif",
                @"c:\files\sun\aaawifihsdifhish.txt",
                @"c:\files\zangetsu\motherland.js",
                @"c:\files\zangetsu\moon\neiborhood.gif",
                @"c:\files\stand\here\comrad\sunday.txt",
                @"c:\files\mimik\spider\man\tongue\control\panel\MilesMorales.js",
                @"c:\files\mimik\spider\man\image.gif",
                @"c:\files\mimik\spider\man\tongue\control\Gokuden.txt",
                @"c:\files\mimik\spider\man\tongue\rainbow.js",
                @"c:\files\mimik\spider\man\tongue\colors\pink.gif",
                @"c:\files\mirror\clone.txt",
                @"c:\dnd\mage.js",
                @"c:\dnd\warrior.gif",
                @"c:\dnd\bard.txt",
                @"c:\dnd\healer.js",
                @"c:\demo\darksidewhy.gif",
                @"c:\fate\astolfo.txt",
                @"c:\fate\Emia.js",
                @"c:\fate\Shiro.gif",
                @"c:\fate\Archer.txt",
                @"c:\fate\Saber.js",
                @"c:\fate\stay\night\unlimited\blade\works\heavens\feel\apocrif\prototype\tsukihime\moon\princess\arkveit.gif",
            };
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { paths[0], new MockFileData("Testing is meh.") },
                { paths[1], new MockFileData("some js") },
                { paths[2], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[3], new MockFileData("Testing is meh.") },
                { paths[4], new MockFileData("some js") },
                { paths[5], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[6], new MockFileData("Testing is meh.") },
                { paths[7], new MockFileData("some js") },
                { paths[8], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[9], new MockFileData("Testing is meh.") },
                { paths[10], new MockFileData("some js") },
                { paths[11], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[12], new MockFileData("Testing is meh.") },
                { paths[13], new MockFileData("some js") },
                { paths[14], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[15], new MockFileData("Testing is meh.") },
                { paths[16], new MockFileData("some js") },
                { paths[17], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[18], new MockFileData("Testing is meh.") },
                { paths[19], new MockFileData("some js") },
                { paths[20], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[21], new MockFileData("Testing is meh.") },
                { paths[22], new MockFileData("some js") },
                { paths[23], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[24], new MockFileData("Testing is meh.") },
                { paths[25], new MockFileData("some js") },
                { paths[26], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
            });
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => true;
            var visitor = new FileSystemVisitor(fileSystem, predicate, initialPath);
            var list = new List<string>();

            await foreach (var name in visitor.Search())
            {
                list.Add(name);
            }

            Assert.True(list.Count == paths.Count && paths.TrueForAll(path => list.Exists(item => item == path.Replace(initialPath, string.Empty))));
        }

        /// <summary>
        /// Checkes do the visitor returns all files that are filtered.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GroupOfMethods_FilteredFiles_Returned()
        {
            var paths = new List<string>
            {
                @"c:\myfile.txt",
                @"c:\demo\jQuery.js",
                @"c:\demo\image.gif",
                @"c:\files\home.txt",
                @"c:\files\jQuery.js",
                @"c:\files\sun\image.gif",
                @"c:\files\sun\aaawifihsdifhish.txt",
                @"c:\files\zangetsu\motherland.js",
                @"c:\files\zangetsu\moon\neiborhood.gif",
                @"c:\files\stand\here\comrad\sunday.txt",
                @"c:\files\mimik\spider\man\tongue\control\panel\MilesMorales.js",
                @"c:\files\mimik\spider\man\image.gif",
                @"c:\files\mimik\spider\man\tongue\control\Gokuden.txt",
                @"c:\files\mimik\spider\man\tongue\rainbow.js",
                @"c:\files\mimik\spider\man\tongue\colors\pink.gif",
                @"c:\files\mirror\clone.txt",
            };
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { paths[0], new MockFileData("Testing is meh.") },
                { paths[1], new MockFileData("some js") },
                { paths[2], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[3], new MockFileData("Testing is meh.") },
                { paths[4], new MockFileData("some js") },
                { paths[5], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[6], new MockFileData("Testing is meh.") },
                { paths[7], new MockFileData("some js") },
                { paths[8], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[9], new MockFileData("Testing is meh.") },
                { paths[10], new MockFileData("some js") },
                { paths[11], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[12], new MockFileData("Testing is meh.") },
                { paths[13], new MockFileData("some js") },
                { paths[14], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
            });
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";
            var visitor = new FileSystemVisitor(fileSystem, predicate, initialPath);
            var list = new List<string>();

            await foreach (var name in visitor.Search())
            {
                list.Add(name);
            }

            Assert.True(list.TrueForAll((path) => paths.Select((name, _) => name.Replace(initialPath, string.Empty)).Where(name => name.Contains(".gif")).Contains(path)));
        }

        /// <summary>
        /// Checkes do the visitor return only filtered records.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GroupOfMethods_OnlyFilteredFiles_Returned()
        {
            var paths = new List<string>
            {
                @"c:\myfile.txt",
                @"c:\demo\jQuery.js",
                @"c:\demo\image.gif",
                @"c:\files\home.txt",
                @"c:\files\jQuery.js",
                @"c:\files\sun\image.gif",
                @"c:\files\sun\aaawifihsdifhish.txt",
                @"c:\files\zangetsu\motherland.js",
                @"c:\files\zangetsu\moon\neiborhood.gif",
                @"c:\files\stand\here\comrad\sunday.txt",
                @"c:\files\mimik\spider\man\tongue\control\panel\MilesMorales.js",
                @"c:\files\mimik\spider\man\image.gif",
                @"c:\files\mimik\spider\man\tongue\control\Gokuden.txt",
                @"c:\files\mimik\spider\man\tongue\rainbow.js",
                @"c:\files\mimik\spider\man\tongue\colors\pink.gif",
                @"c:\files\mirror\clone.txt",
            };
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { paths[0], new MockFileData("Testing is meh.") },
                { paths[1], new MockFileData("some js") },
                { paths[2], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[3], new MockFileData("Testing is meh.") },
                { paths[4], new MockFileData("some js") },
                { paths[5], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[6], new MockFileData("Testing is meh.") },
                { paths[7], new MockFileData("some js") },
                { paths[8], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[9], new MockFileData("Testing is meh.") },
                { paths[10], new MockFileData("some js") },
                { paths[11], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[12], new MockFileData("Testing is meh.") },
                { paths[13], new MockFileData("some js") },
                { paths[14], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
            });
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";
            var visitor = new FileSystemVisitor(fileSystem, predicate, initialPath);
            var list = new List<string>();

            await foreach (var name in visitor.Search())
            {
                list.Add(name);
            }

            Assert.True(list.TrueForAll((path) => !paths.Select((name, _) => name.Replace(initialPath, string.Empty)).Where(name => !name.Contains(".gif")).Contains(path)));
        }

        /// <summary>
        /// Checkes do the visitor stop if filtered file found and Stop flag is set.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GroupOfMethods_Stoped_Returned()
        {
            var paths = new List<string>
            {
                @"c:\myfile.txt",
                @"c:\demo\jQuery.js",
                @"c:\demo\image.gif",
                @"c:\files\home.txt",
                @"c:\files\jQuery.js",
                @"c:\files\sun\image.gif",
                @"c:\files\sun\aaawifihsdifhish.txt",
                @"c:\files\zangetsu\motherland.js",
                @"c:\files\zangetsu\moon\neiborhood.gif",
                @"c:\files\stand\here\comrad\sunday.txt",
                @"c:\files\mimik\spider\man\tongue\control\panel\MilesMorales.js",
                @"c:\files\mimik\spider\man\image.gif",
                @"c:\files\mimik\spider\man\tongue\control\Gokuden.txt",
                @"c:\files\mimik\spider\man\tongue\rainbow.js",
                @"c:\files\mimik\spider\man\tongue\colors\pink.gif",
                @"c:\files\mirror\clone.txt",
            };
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { paths[0], new MockFileData("Testing is meh.") },
                { paths[1], new MockFileData("some js") },
                { paths[2], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[3], new MockFileData("Testing is meh.") },
                { paths[4], new MockFileData("some js") },
                { paths[5], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[6], new MockFileData("Testing is meh.") },
                { paths[7], new MockFileData("some js") },
                { paths[8], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[9], new MockFileData("Testing is meh.") },
                { paths[10], new MockFileData("some js") },
                { paths[11], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { paths[12], new MockFileData("Testing is meh.") },
                { paths[13], new MockFileData("some js") },
                { paths[14], new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
            });
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";
            var visitor = new FileSystemVisitor(fileSystem, predicate, initialPath);
            visitor.FilteredFileFound += (senger, args) => args.Stop = true;
            var list = new List<string>();

            await foreach (var name in visitor.Search())
            {
                list.Add(name);
            }

            Assert.True(list.Count == 1);
        }
    }
}
