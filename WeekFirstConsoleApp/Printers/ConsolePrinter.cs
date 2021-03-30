using System;
using WeekFirstConsoleApp.Validators;

namespace WeekFirstConsoleApp.Printers
{
    public class ConsolePrinter
    {
        public static void AskNameToPrint()
        {
            var args = Environment.GetCommandLineArgs();
            var validator = new NameValidator();
            if (args?.Length > 1)
            {
                var name = args[1];
                if (validator.Validate(name))
                {
                    Console.WriteLine($"Hello, {name}!");
                }
                else
                {
                    Console.WriteLine("Name should start with upper case symbol. Try again...");
                }
            }
            else
            {
                Console.WriteLine($"Hello, {Environment.UserName}!");
            }

            Console.ReadKey();
        }
    }
}
