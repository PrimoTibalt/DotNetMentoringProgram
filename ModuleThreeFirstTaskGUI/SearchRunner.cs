using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
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
        private readonly MainWindow _window;
        private Func<IFileSystemInfo, bool> _predicate;

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

            _window = window;
        }

        private string FilePath { get; set; }

        /// <summary>
        /// Add path for FileSystemVisitor constructor.
        /// </summary>
        /// <param name="path"></param>
        public string AddFilePath(string path)
        {
            FilePath = string.IsNullOrWhiteSpace(path) ? @"c:\" : path;
            return FilePath;
        }

        /// <summary>
        /// Set new filter for name of the file.
        /// </summary>
        /// <param name="predicate"></param>
        public void SetFilterForName(Func<IFileSystemInfo, bool> predicate)
        {
            if (predicate is null)
            {
                predicate = (info) => info.Name is string;
            }

            _predicate = predicate;
        }

        /// <summary>
        /// Run search async.
        /// </summary>
        public void RunSearch()
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
            var visitor = new FileSystemVisitor(fileSystem, _predicate);
            var list = new List<string>();
            foreach (var result in visitor.Search(FilePath))
            {
                Dispatcher.Invoke(() => _window.FilesView.Items.Add(result));
            }
        }
    }
}
