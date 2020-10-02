using System;
using System.Net.Mail;
using System.Text;
using Timeviewer;

namespace Overtimer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            RemoveController user = RemoveController.GetUser();
            Console.WriteLine($"ID:{user.Username}");
            Console.WriteLine($"Pass:{user.Password}");
            Console.WriteLine($"User:{user.Holder}");
            Console.ReadKey();
        }
    }
}
