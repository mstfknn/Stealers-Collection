namespace FileGrabber
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal static class Program
    {
        // Universal File Grabber
        // Author: Antlion
        // https://github.com/r3xq1

        private static readonly Stopwatch sw = Stopwatch.StartNew();
        private static void Main()
        {
            Console.Title = "Universal File Grabber";
            Task.Factory.StartNew(() => { GetFiles.Inizialize(); }).Wait();
            sw.Stop();
            Console.WriteLine("Копирование завершено!");
            Console.WriteLine($"Затраченное время: {sw.Elapsed.TotalMilliseconds} мс");
            //File.AppendAllText("FastTime.txt", $"Затраченное время: {sw.Elapsed.TotalMilliseconds} мс");
            Console.ReadKey();
        }
    }
}