using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {
             Console.WriteLine(PathFireFox.GetLocationFireFox());
            // Console.WriteLine(PathFireFox.GetRegistryFireFox());
            Console.ReadKey();
        }
    }
}