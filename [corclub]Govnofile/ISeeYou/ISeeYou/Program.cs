using System;
using System.Windows.Forms;

namespace ISeeYou
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //bool createdNew;
            //new Mutex(true, "[mutex]", out createdNew);
            //if (!createdNew)
            //{
            //    Application.Exit();
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
