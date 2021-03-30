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
            var output = GetGreating();
            Greating.Content = output;
        }

        public string GetGreating()
        {
            var args = Environment.GetCommandLineArgs();
            var validator = new NameValidator();
            var text = "";
            if (args?.Length > 1)
            {
                var name = args[1];
                if (validator.Validate(name))
                {
                    text = $"Hello, {name}!";
                }
            }
            else
            {
                text = $"Hello, {Environment.UserName}!";
            }

            return text;
        }
    }
}
