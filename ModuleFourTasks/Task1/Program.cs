using System;
using System.Linq;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string finishWords = @"Click any button to close application.";
            if (args.Length < 1)
            {
                Console.WriteLine("No arguments were provided. Try again...");
            }
            else
            {
                args.ToList().ForEach((item) => Console.Write(item[0]));
                Console.WriteLine();
            }

            Console.WriteLine(finishWords);
            Console.ReadKey();
        }
    }
}
