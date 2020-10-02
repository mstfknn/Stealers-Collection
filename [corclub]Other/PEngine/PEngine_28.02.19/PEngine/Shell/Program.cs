namespace PEngine
{
    using System;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.Run(new Updater());
        }
    }
}