using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModuleThreeFirstTaskGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchRunner _runner;

        /// <summary>
        /// Constructor of WPF page.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(_runner.RunSearch);
            SearchBtn.IsEnabled = false;
            ApplyBtn.IsEnabled = false;
            ApplyFilterBtn.IsEnabled = false;
            WaitingLabel.Visibility = Visibility.Visible;
            FilesView.Items.Clear();
            task.ContinueWith((t) => Dispatcher.Invoke(() =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }

                SearchBtn.IsEnabled = true;
                ApplyBtn.IsEnabled = true;
                ApplyFilterBtn.IsEnabled = true;
                WaitingLabel.Visibility = Visibility.Hidden;
            }));
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilterBtn.Visibility = Visibility.Visible;
            SearchBtn.Visibility = Visibility.Visible;
            _runner = new SearchRunner(this);
            FilePathLbl.Content = $"Current path: {_runner.AddFilePath(PathTxt.Text)}";
        }

        private void ApplyFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            var upperFilterText = FilterContainsTxt.Text.ToUpperInvariant();
            _runner.SetFilterForName((file) => Dispatcher.Invoke(() => file.Name.ToUpperInvariant().Contains(upperFilterText)));
        }
    }
}
