// <copyright file="FileSystemVisitor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace ModuleThreeFirstTaskConsole
{
    /// <summary>
    /// Can read filesystem and show documents and packages in linear form.
    /// </summary>
    public class FileSystemVisitor
    {
        private const string STANDARDWINDOWSPATH = @"C:\";
        private const string STANDARDLINUXPATH = @"/";

        private readonly IFileSystem _fileSystem;
        private readonly Func<IFileSystemInfo, bool> _filter;
        private string _initialPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemVisitor"/> class.
        /// </summary>
        /// <param name="fileSystem">Files and directories.</param>
        /// <param name="filter">Filter of files and directories.</param>
        public FileSystemVisitor(
            IFileSystem fileSystem,
            Func<IFileSystemInfo, bool> filter)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            _filter = filter is null ? x => true : filter;
            Stoped = true;
        }

        /// <summary>
        /// Events that happen at the begin of search.
        /// </summary>
        public event Action<IFileSystemInfo> SearchStarted;

        /// <summary>
        /// Events that happen at the end of search.
        /// </summary>
        public event Action<IFileSystemInfo> SearchEnded;

        /// <summary>
        /// Events that happen when file is found.
        /// </summary>
        public event Action<FileSystemEventArgs> FileFound;

        /// <summary>
        /// Events that happen when directory is found.
        /// </summary>
        public event Action<FileSystemEventArgs> DirectoryFound;

        /// <summary>
        /// Events that happen when filtered file is found.
        /// </summary>
        public event Action<FileSystemEventArgs> FilteredFileFound;

        /// <summary>
        /// Events that happen when filtered directory is found.
        /// </summary>
        public event Action<FileSystemEventArgs> FilteredDirectoryFound;

        /// <summary>
        /// State of searching. If it's true on the next step except for search start the visitor will not proceed to work.
        /// </summary>
        public bool Stoped { get; set; }

        /// <summary>
        /// Search files in provided fileSystem.
        /// </summary>
        /// <returns>Files from directories.</returns>
        /// <param name="initialPath">Place in filesystem where to start searching.</param>
        public IEnumerable<string> Search(string initialPath)
        {
            SetInitialPath(initialPath);
            var initialDiractoryName = _fileSystem.Path.GetDirectoryName(_initialPath);
            var directory = _fileSystem.DirectoryInfo.FromDirectoryName(initialDiractoryName ?? _fileSystem.Directory.GetCurrentDirectory());
            OnSearchStarted(directory);
            foreach (var info in GetAllFromCurrentDirectory(directory))
            {
                yield return GetNameWithoutLongPath(info);
                if (Stoped)
                {
                    break;
                }
            }

            OnSearchEnded(directory);
        }

        /// <summary>
        /// Returns every single directory and filtered files from sprecified directory.
        /// </summary>
        /// <param name="directory">Provided directory.</param>
        /// <returns>FileInfo of each file and directory.</returns>
        private IEnumerable<IFileSystemInfo> GetAllFromCurrentDirectory(IFileSystemInfo directory)
        {
            foreach (var directoryName in _fileSystem.Directory.EnumerateDirectories(directory.FullName))
            {
                var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(directoryName);
                var results = ProcessFileSystemObject(directoryInfo);
                if (!results.Exclude)
                {
                    foreach (var file in GetAllFromCurrentDirectory(directoryInfo))
                    {
                        yield return file;
                    }

                    foreach (var file in GetFilesFromCurrentDirectory(directoryInfo))
                    {
                        yield return file;
                    }
                }
            }

            if (directory.FullName.Equals(STANDARDWINDOWSPATH))
            {
                foreach (var file in ProcessRootDirectory(directory))
                {
                    yield return file;
                }
            }
        }

        private IEnumerable<IFileSystemInfo> ProcessRootDirectory(IFileSystemInfo directory)
        {
            var results = ProcessFileSystemObject(directory);
            if (!results.Exclude)
            {
                foreach (var file in GetFilesFromCurrentDirectory(directory))
                {
                    yield return file;
                }
            }
        }

        private void OnSearchStarted(IFileSystemInfo info)
        {
            Stoped = false;
            SearchStarted?.Invoke(info);
        }

        private void OnSearchEnded(IFileSystemInfo info)
        {
            Stoped = true;
            SearchEnded?.Invoke(info);
        }

        private FileSystemEventArgs ProcessFileSystemObject(IFileSystemInfo info)
        {
            var isDirecotry = info.Attributes.HasFlag(FileAttributes.Directory);
            var isIgnored = isDirecotry ? false : !_filter(info);
            var args = new FileSystemEventArgs(info);

            var handler = isDirecotry
                ? isIgnored
                    ? DirectoryFound
                    : FilteredDirectoryFound
                : isIgnored
                    ? FileFound
                    : FilteredFileFound;

            handler?.Invoke(args);
            args.Exclude = args.Exclude || isIgnored;
            Stoped = args.Stop;
            return args;
        }

        private IEnumerable<IFileSystemInfo> GetFilesFromCurrentDirectory(IFileSystemInfo directoryInfo)
        {
            foreach (var fileName in _fileSystem.Directory.EnumerateFiles(directoryInfo.FullName))
            {
                var fileInfo = _fileSystem.FileInfo.FromFileName(fileName);
                var result = ProcessFileSystemObject(fileInfo);
                if (!result.Exclude)
                {
                    yield return fileInfo;
                }
            }
        }

        private string GetNameWithoutLongPath(IFileSystemInfo file)
        {
            return file.FullName.Replace(_initialPath, string.Empty);
        }

        private void SetInitialPath(string initialPath)
        {
            string standardPath = null;
            if (_fileSystem.Directory.Exists(STANDARDWINDOWSPATH))
            {
                standardPath = STANDARDWINDOWSPATH;
            }

            if (_fileSystem.Directory.Exists(STANDARDWINDOWSPATH))
            {
                standardPath = STANDARDLINUXPATH;
            }

            _initialPath = initialPath ?? standardPath ?? throw new ArgumentException("File system isn't linux or windows like.");
            _initialPath = _fileSystem.Directory.Exists(initialPath) ? initialPath : throw new DirectoryNotFoundException("You entered path that doesn't exist.");
        }
    }
}
