using System;

namespace MozillaDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(PathFireFox.GetLocationFireFox());
             Console.WriteLine(PathFireFox.GetRandomFF());
           DecMozilla.InitializeDelegates();
            //GetPassword.Old_FF();

            // DecMozilla.InitializeDelegates();
            Console.ReadKey();
        }
    }
}