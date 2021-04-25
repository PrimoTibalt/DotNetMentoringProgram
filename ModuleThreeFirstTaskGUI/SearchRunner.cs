using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ModuleThreeFirstTaskConsole;

namespace ModuleThreeFirstTaskGUI
{
    /// <summary>
    /// Need to run search with FileSystemVisitor.
    /// </summary>
    public class SearchRunner : Window
    {
        private readonly MainWindow window;
        private string filePath;
        private Predicate<FileSystemInfo> predicate;

        /// <summary>
        /// Use window to change state of screen from outer type.
        /// Throws ArgumentNullException on null in window.
        /// </summary>
        /// <param name="window"></param>
        public SearchRunner(MainWindow window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            this.window = window;
        }

        /// <summary>
        /// Add path for FileSystemVisitor constructor.
        /// </summary>
        /// <param name="path"></param>
        public string AddFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = @"c:\";
            }

            filePath = path;
            return filePath;
        }

        /// <summary>
        /// Set new filter for name of the file.
        /// </summary>
        /// <param name="predicate"></param>
        public void SetFilterForName(Predicate<FileSystemInfo> predicate)
        {
            if (predicate is null)
            {
                predicate = (info) => info.Name is string;
            }

            this.predicate = predicate;
        }

        /// <summary>
        /// Run search async.
        /// </summary>
        public async Task RunSearch()
        {
            var visitor = new FileSystemVisitor(new DirectoryInfo(filePath), predicate);
            var list = new List<string>();
            await foreach (var result in visitor.Search())
            {
                Dispatcher.Invoke(() => window.FilesView.Items.Add(result));
            }
        }
    }
}
