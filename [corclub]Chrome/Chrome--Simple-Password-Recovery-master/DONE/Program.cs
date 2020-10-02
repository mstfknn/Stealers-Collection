using System;

namespace SimpleChromepwrec
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Field kek in Chrome.GetSavedPasswords())
            {
                Console.WriteLine($"SITE: {kek.URL}");
                Console.WriteLine($"USER: {kek.Username}");
                Console.WriteLine($"PASSWORD: {kek.Password}");
                Console.WriteLine(Environment.NewLine);
            }

            Console.ReadKey();

        }
    }
}