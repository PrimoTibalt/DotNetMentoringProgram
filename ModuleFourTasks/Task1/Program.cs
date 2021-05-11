using System;
using System.Linq;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter words for abbreviation in one line.");
            var result = Console.ReadLine();
            var words = result.Trim().Split(' ').ToList();
            words.RemoveAll(item => string.IsNullOrWhiteSpace(item));
            if (words.Count < 1)
            {
                Console.WriteLine("No arguments were provided. Try again...");
                return;
            }

            words.ForEach(item => Console.Write(item[0]));
            Console.WriteLine();
            Console.WriteLine("Press any button to close app.");
            Console.ReadKey();
        }
    }
}
