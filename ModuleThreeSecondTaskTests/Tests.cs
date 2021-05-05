using System;
using System.Collections.Generic;
using System.IO;
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
        [Fact]
        public void Method_AllFiles_Returned()
        {
            var files = new Dictionary<string, MockFileData>
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
                { @"c:\fate\stay\night\unlimited\blade\works\heavens\feel\apocrif\prototype\tsukihime\moon\princess\arkveit.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => true;
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.True(list.Count == paths.Count && paths.TrueForAll(path => list.Exists(item => item == path.Replace(initialPath, string.Empty))));
        }

        /// <summary>
        /// Checkes do the visitor returns all files that are filtered.
        /// </summary>
        [Fact]
        public void GroupOfMethods_FilteredFiles_Returned()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.True(list.TrueForAll((path) => paths.Select((name, _) => name.Replace(initialPath, string.Empty)).Where(name => name.Contains(".gif")).Contains(path)));
        }

        /// <summary>
        /// Checkes do the visitor return only filtered records.
        /// </summary>
        [Fact]
        public void GroupOfMethods_OnlyFilteredFiles_Returned()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.True(list.TrueForAll((path) => !paths.Select((name, _) => name.Replace(initialPath, string.Empty)).Where(name => !name.Contains(".gif")).Contains(path)));
        }

        /// <summary>
        /// Checkes do the visitor stop if filtered file found and Stop flag is set.
        /// </summary>
        [Fact]
        public void GroupOfMethods_WithStopFlagOnFilterFileFound_ReturnedOneFile()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            visitor.FilteredFileFound += (args) => args.Stop = true;
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.True(list.Count == 1);
        }

        /// <summary>
        /// Checkes do the visitor returned path that fits filtering.
        /// </summary>
        [Fact]
        public void GroupOfMethods_WithStopFlagOnFilterFileFound_ReturnedFileFitsCondition()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\";
            string extension = ".gif";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            visitor.FilteredFileFound += (args) => args.Stop = true;
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.Contains(extension, list[0]);
        }

        /// <summary>
        /// Checkes do the visitor throw ArgumentNullException if filesystem is null.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public void Method_NullInFileSystem_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null, (info) => true));
        }

        /// <summary>
        /// Checkes do the visitor throw DirectoryNotFoundException if initialpath doesn't exist.
        /// </summary>
        [Fact]
        public void Method_WrongInitialPath_Throws()
        {
            var files = new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                { @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) },
                { @"c:\files\home.txt", new MockFileData("Testing is meh.") },
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\monkey";
            string extension = ".gif";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;
            List<string> list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);

            Assert.Throws<DirectoryNotFoundException>(() => visitor.Search(initialPath).ToList());
        }

        /// <summary>
        /// Checkes do the visitor return files from long initialpath.
        /// </summary>
        [Fact]
        public void GroupOfMethods_LongInitialPath_ReturnedFilesFromThePath()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\files\mimik\spider\man\tongue";
            var shortPathsFiltered = paths.Where(path => path.Contains(initialPath)).Select((path, _) => path.Replace(initialPath, string.Empty)).ToList();
            Func<IFileSystemInfo, bool> predicate = (info) => true;
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.True(list.TrueForAll(file => shortPathsFiltered.IndexOf(file) != -1) && list.Count == shortPathsFiltered.Count);
        }

        /// <summary>
        /// Checkes do the visitor return files only if they passed validation and not excluded.
        /// </summary>
        [Fact]
        public void Class_FilterAndExcludeConditionsProvided_ReturnedOnlyFilteredAndNotExcludedFiles()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\files";
            string extension = ".txt";
            string directory = "mimik";
            var shortPathsFiltered = paths.Where(path => path.Contains(initialPath) && path.Contains(directory) && path.Contains(extension)).Select((path, _) => path.Replace(initialPath, string.Empty)).ToList();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;
            var list = new List<string>();

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            visitor.FilteredFileFound += (args) => args.Exclude = !args.Info.FullName.Contains(directory);
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.True(list.TrueForAll(file => shortPathsFiltered.IndexOf(file) != -1) && list.Count == shortPathsFiltered.Count);
        }

        /// <summary>
        /// Checkes do the visitor return files only if they passed validation and not excluded.
        /// </summary>
        [Fact]
        public void Class_OnFilteredFileFoundCounter_CountsRightTimes()
        {
            var files = new Dictionary<string, MockFileData>
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
            };
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            string initialPath = @"c:\files";
            string extension = ".txt";
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;
            var list = new List<string>();
            var counter = 0;

            var visitor = new FileSystemVisitor(fileSystem, predicate);
            visitor.FilteredFileFound += (args) => counter++;
            foreach (var name in visitor.Search(initialPath))
            {
                list.Add(name);
            }

            Assert.Equal(list.Count, counter);
        }
    }
}
