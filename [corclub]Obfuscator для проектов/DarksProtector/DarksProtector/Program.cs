using System;
using System.Windows.Forms;

namespace DarksProtector
{
    internal static partial class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DarksProtectorForm());
        }
    }
}