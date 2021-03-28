using System;
using WeekFirstConsoleApp.Validators;

namespace WeekFirstConsoleApp.Printers
{
    public class ConsolePrinter
    {
        public static void AskNameToPrint()
        {
            Console.WriteLine("Enter your name: ");
            var name = Console.ReadLine();
            var validator = new NameValidator();
            Console.Clear();
            if (validator.Validate(name))
            {
                Console.WriteLine($"Hello, {name}!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Name should be without white spaces, digits or special symbols. Start the name with upper case symbol. Try again...");
                Console.ReadKey();
                ConsolePrinter.AskNameToPrint();
            }

            return;
        }
    }
}
