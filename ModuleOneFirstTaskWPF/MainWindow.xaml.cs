// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ModuleOneFirstTaskWPF
{
    using System.Windows;
    using ModuleOneSecondTaskLibrary;

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
            InitializeComponent();
            ShowMessage();
        }

        /// <summary>
        /// Shows some message on screen.
        /// </summary>
        public void ShowMessage()
        {
            Greating.Content = GreatingBuilder.Build();
        }
    }
}
