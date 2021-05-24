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
            var files = FileSystemDictionaryFactory.CreateLargeFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\";
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => true;

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
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\";
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";

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
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\";
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";

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
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\";
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == ".gif";

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
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\";
            var extension = ".gif";
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;

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
            var files = FileSystemDictionaryFactory.CreateSmallFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\monkey";
            var extension = ".gif";
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;

            var visitor = new FileSystemVisitor(fileSystem, predicate);

            Assert.Throws<DirectoryNotFoundException>(() => visitor.Search(initialPath).ToList());
        }

        /// <summary>
        /// Checkes do the visitor return files from long initialpath.
        /// </summary>
        [Fact]
        public void GroupOfMethods_LongInitialPath_ReturnedFilesFromThePath()
        {
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\files\mimik\spider\man\tongue";
            var shortPathsFiltered = paths.Where(path => path.Contains(initialPath)).Select((path, _) => path.Replace(initialPath, string.Empty)).ToList();
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => true;

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
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\files";
            var extension = ".txt";
            var directory = "mimik";
            var shortPathsFiltered = paths.Where(path => path.Contains(initialPath) && path.Contains(directory) && path.Contains(extension)).Select((path, _) => path.Replace(initialPath, string.Empty)).ToList();
            var list = new List<string>();
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;

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
            var files = FileSystemDictionaryFactory.CreateMiddleFileSystem();
            var paths = files.Keys.ToList();
            var fileSystem = new MockFileSystem(files);
            var initialPath = @"c:\files";
            var extension = ".txt";
            var list = new List<string>();
            var counter = 0;
            Func<IFileSystemInfo, bool> predicate = (info) => info.Extension == extension;

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
