using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Confuser.Runtime
{
    internal static class MD5
    {
        static void Initialize()
        {
            using (Stream bas = new StreamReader(typeof(MD5).Assembly.Location).BaseStream)
            {
                var file = new BinaryReader(bas);
                byte[] file2 = File.ReadAllBytes(typeof(MD5).Assembly.Location);
                byte[] byt = file.ReadBytes(file2.Length - 32);
                string a = Hash(byt);
                file.BaseStream.Position = file.BaseStream.Length - 32;
                string b = Encoding.ASCII.GetString(file.ReadBytes(32));

                if (a != b)
                {
                    MessageBox.Show("You probably know what this mean, if not, i'll tell u, you modified the app so DarksProtector is mad :)", "DarksProtector - dark#5000", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private static string Hash(byte[] hash)
        {
            StringBuilder sb = null;
#pragma warning disable SCS0006 // Weak hashing function
            using (var md5 = new MD5CryptoServiceProvider())
#pragma warning restore SCS0006 // Weak hashing function
            {
                byte[] btr = hash;
                btr = md5.ComputeHash(btr);
                sb = new StringBuilder();

                foreach (byte ba in btr)
                {
                    sb.Append(ba.ToString("x2").ToLower());
                }
            }
            return sb.ToString();
        }
    }
}
