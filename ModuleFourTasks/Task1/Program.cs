using System;
using System.Linq;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter lines for abbreviation.");
            var result = string.Empty;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == '.')
                {
                    break;
                }

                result += key.KeyChar.ToString();
            }

            var words = result.Trim().Replace("\0", string.Empty).Split('\r').ToList();
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
