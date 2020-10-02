using System;
using System.Reflection;
using System.Windows.Forms;

namespace GetFirefoxPasswords
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            string resource = "GetFirefoxPasswords.System.Data.SQLite.dll";
            string resource2 = "GetFirefoxPasswords.ObjectListView.dll";
            EmbeddedAssembly.Load(resource, "System.Data.SQLite.dll");
            EmbeddedAssembly.Load(resource2, "ObjectListView.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}