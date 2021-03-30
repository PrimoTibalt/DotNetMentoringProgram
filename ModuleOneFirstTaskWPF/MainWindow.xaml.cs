// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ModuleOneFirstTaskWPF
{
    using System;
    using System.Windows;
    using WeekFirstConsoleApp.Validators;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.ShowMessage();
        }

        /// <summary>
        /// Shows some message on screen.
        /// </summary>
        public void ShowMessage()
        {
            var output = this.GetGreating();
            this.Greating.Content = output;
        }

        /// <summary>
        /// Creates greeting text with name from console attributes or if it wasn't provided with computer name.
        /// </summary>
        /// <returns>Greating text.</returns>
        public string GetGreating()
        {
            var args = Environment.GetCommandLineArgs();
            var validator = new NameValidator();
            var text = string.Empty;
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
