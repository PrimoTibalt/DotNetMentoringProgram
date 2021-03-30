using System;
using System.Windows;
using WeekFirstConsoleApp.Validators;

namespace Week1WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowMessage();
        }

        public void ShowMessage()
        {
            var args = Environment.GetCommandLineArgs();
            var validator = new NameValidator();
            if (args?.Length > 1)
            {
                var name = args[1];
                if (validator.Validate(name))
                {
                    Greating.Content = $"Hello, {name}!";
                }
            }
            else
            {
                Greating.Content = $"Hello, {Environment.UserName}!";
            }
        }
    }
}
