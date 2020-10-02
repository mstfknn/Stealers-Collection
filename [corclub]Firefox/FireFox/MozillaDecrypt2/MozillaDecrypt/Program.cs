using System;

namespace MozillaDecrypt
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DecMozilla.InitializeDelegates();
            Console.ReadKey();
        }
    }
}