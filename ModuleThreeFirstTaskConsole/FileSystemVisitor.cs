// <copyright file="FileSystemVisitor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;

namespace ModuleThreeFirstTaskConsole
{
    /// <summary>
    /// Can read filesystem and show documents and packages in linear form.
    /// </summary>
    public class FileSystemVisitor
    {
        private readonly DirectoryInfo fileSystem;
        private readonly Predicate<FileSystemInfo> filter;
        private bool stoped = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemVisitor"/> class.
        /// </summary>
        /// <param name="fileSystem">Files and directories.</param>
        /// <param name="filter">Filter of files and directories.</param>
        public FileSystemVisitor(DirectoryInfo fileSystem, Predicate<FileSystemInfo> filter)
        {
            if (fileSystem is null)
            {
                throw new NullReferenceException(nameof(fileSystem));
            }
            else if (Directory.Exists(fileSystem.FullName))
            {
                this.fileSystem = fileSystem;
            }
            else
            {
                throw new DirectoryNotFoundException();
            }

            this.filter = filter is null ? x => true : filter;
        }

        /// <summary>
        /// Events that happen at the begin of search.
        /// </summary>
        public event EventHandler<FileSystemInfo> SearchStarted;

        /// <summary>
        /// Events that happen at the end of search.
        /// </summary>
        public event EventHandler<FileSystemInfo> SearchEnded;

        /// <summary>
        /// Events that happen when file is found.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FileFound;

        /// <summary>
        /// Events that happen when directory is found.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> DirectoryFound;

        /// <summary>
        /// Events that happen when filtered file is found.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FilteredFileFound;

        /// <summary>
        /// Events that happen when filtered directory is found.
        /// </summary>
        public event EventHandler<FileSystemEventArgs> FilteredDirectoryFound;

        /// <summary>
        /// Search files in provided fileSystem.
        /// </summary>
        /// <returns>Files from directories.</returns>
        public IEnumerable<string> Search()
        {
            OnSearchStarted(fileSystem);
            foreach (var info in GetAllFromCurrentDirectory(fileSystem))
            {
                if (stoped)
                {
                    break;
                }

                yield return GetNameWithoutLongPath(info);
            }

            OnSearchEnded(fileSystem);
        }

        /// <summary>
        /// Invoke methods from SearchStarted event.
        /// </summary>
        /// <param name="info">Provided info.</param>
        protected virtual void OnSearchStarted(FileSystemInfo info)
        {
            SearchStarted?.Invoke(this, info);
        }

        /// <summary>
        /// Invoke methods from SearchEnded event.
        /// </summary>
        /// <param name="info">Provided info.</param>
        protected virtual void OnSearchEnded(FileSystemInfo info)
        {
            stoped = false;
            SearchEnded?.Invoke(this, info);
        }

        /// <summary>
        /// Invoke methods from FileFound event.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnFileFound(FileSystemEventArgs args)
        {
            FileFound?.Invoke(this, args);
            stoped = args.Stop ? args.Stop : stoped;
        }

        /// <summary>
        /// Invoke methods from DirectoryFound event.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnDirectoryFound(FileSystemEventArgs args)
        {
            DirectoryFound?.Invoke(this, args);
            stoped = args.Stop ? args.Stop : stoped;
        }

        /// <summary>
        /// Invoke methods from FilteredFileFound event.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnFilteredFileFound(FileSystemEventArgs args)
        {
            FilteredFileFound?.Invoke(this, args);
            stoped = args.Stop ? args.Stop : stoped;
        }

        /// <summary>
        /// Invoke methods from FilteredDirectoryFound event.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnFilteredDirectoryFound(FileSystemEventArgs args)
        {
            FilteredDirectoryFound?.Invoke(this, args);
            stoped = args.Stop ? args.Stop : stoped;
        }

        /// <summary>
        /// Returns everysingle directory and filtered files from sprecified directory.
        /// </summary>
        /// <param name="directory">Provided directory.</param>
        /// <returns>FileInfo of each file and directory.</returns>
        private IEnumerable<FileSystemInfo> GetAllFromCurrentDirectory(DirectoryInfo directory)
        {
            foreach (var directoryFromDirectory in directory.EnumerateDirectories())
            {
                var eventArgs = new FileSystemEventArgs
                {
                    Info = directoryFromDirectory
                };
                OnDirectoryFound(eventArgs);
                if (eventArgs.Exclude == false && filter(directoryFromDirectory))
                {
                    OnFilteredDirectoryFound(eventArgs);
                    if (eventArgs.Exclude == false)
                    {
                        foreach (var file in GetAllFromCurrentDirectory(directoryFromDirectory))
                        {
                            yield return file;
                        }

                        foreach (var file in GetFilesFromCurrentDirectory(directoryFromDirectory))
                        {
                            yield return file;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns files from current directory.
        /// </summary>
        /// <param name="directoryInfo">Current direcotry.</param>
        /// <returns>Files from directory.</returns>
        private IEnumerable<FileInfo> GetFilesFromCurrentDirectory(DirectoryInfo directoryInfo)
        {
            foreach (var file in directoryInfo.EnumerateFiles())
            {
                var eventArgs = new FileSystemEventArgs
                {
                    Info = file
                };
                OnFileFound(eventArgs);
                if (filter(file) && eventArgs.Exclude == false)
                {
                    OnFilteredFileFound(eventArgs);
                    if (eventArgs.Exclude == false)
                    {
                        yield return file;
                    }
                }
            }
        }

        /// <summary>
        /// To get name with directories started from provided fileSystem.
        /// </summary>
        /// <param name="file">File.</param>
        /// <returns>Correctly cut name.</returns>
        private string GetNameWithoutLongPath(FileSystemInfo file)
        {
            return file.FullName.Replace(fileSystem.FullName, string.Empty);
        }
    }
}
