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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = InputText.Text;
            var validator = new NameValidator();
            if (validator.Validate(name))
            {
                OutputText.Content = $"Hello, {name}!";
            }
            else
            {
                OutputText.Content = "Name should be without white spaces, digits or special symbols. Start the name with upper case symbol. Try again...";
            }
        }
    }
}
