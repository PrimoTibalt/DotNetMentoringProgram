using System;
using System.IO.Abstractions;

namespace ModuleThreeFirstTaskConsole
{
    /// <summary>
    /// Arguments for founded file.
    /// </summary>
    public class FileSystemEventArgs : EventArgs
    {
        /// <summary>
        /// Sets info property.
        /// </summary>
        /// <param name="info"></param>
        public FileSystemEventArgs(IFileSystemInfo info)
        {
            Info = info;
        }

        /// <summary>
        /// Information about file or directory.
        /// </summary>
        public IFileSystemInfo Info { get; }

        /// <summary>
        /// If checked - Search action will be stopped.
        /// </summary>
        public bool Stop { get; set; }

        /// <summary>
        /// If checked - this file will be excluded from output.
        /// </summary>
        public bool Exclude { get; set; }
    }
}
