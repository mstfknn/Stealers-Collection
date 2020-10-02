using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords
{
	// Token: 0x0200004B RID: 75
	public class SafariPassword
	{
		// Token: 0x06000209 RID: 521
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int LocalFree(IntPtr hMem);

		// Token: 0x0600020A RID: 522
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref SafariPassword.DATA_BLOB pDataIn, IntPtr ppszDataDescr, ref SafariPassword.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref SafariPassword.DATA_BLOB pDataOut);

		// Token: 0x0600020B RID: 523 RVA: 0x000088C0 File Offset: 0x00006AC0
		internal SafariPassword(string server, string user, byte[] password)
		{
			this.salt = new byte[]
			{
				29,
				172,
				168,
				248,
				211,
				184,
				72,
				62,
				72,
				125,
				62,
				10,
				98,
				7,
				221,
				38,
				230,
				103,
				129,
				3,
				231,
				178,
				19,
				165,
				176,
				121,
				238,
				79,
				15,
				65,
				21,
				237,
				123,
				20,
				140,
				229,
				75,
				70,
				13,
				193,
				142,
				254,
				214,
				231,
				39,
				117,
				6,
				139,
				73,
				0,
				220,
				15,
				48,
				160,
				158,
				253,
				9,
				133,
				241,
				200,
				170,
				117,
				193,
				8,
				5,
				121,
				1,
				226,
				151,
				216,
				175,
				128,
				56,
				96,
				11,
				113,
				14,
				104,
				83,
				119,
				47,
				15,
				97,
				246,
				29,
				142,
				143,
				92,
				178,
				61,
				33,
				116,
				64,
				75,
				181,
				6,
				110,
				171,
				122,
				189,
				139,
				169,
				126,
				50,
				143,
				110,
				6,
				36,
				217,
				41,
				164,
				165,
				190,
				38,
				35,
				253,
				238,
				241,
				76,
				15,
				116,
				94,
				88,
				251,
				145,
				116,
				239,
				145,
				99,
				111,
				109,
				46,
				97,
				112,
				112,
				108,
				101,
				46,
				83,
				97,
				102,
				97,
				114,
				105
			};
			this.m_Server = server;
			this.m_UserName = user;
			this.m_PasswordValue = password;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008D24 File Offset: 0x00006F24
		public string Server
		{
			get
			{
				return this.m_Server;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00008D3C File Offset: 0x00006F3C
		public string UserNameValue
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00008D54 File Offset: 0x00006F54
		public string PasswordValue
		{
			get
			{
				IntPtr intPtr = Marshal.AllocHGlobal(this.m_PasswordValue.Length);
				IntPtr intPtr2 = Marshal.AllocHGlobal(this.salt.Length);
				Marshal.Copy(this.m_PasswordValue, 0, intPtr, this.m_PasswordValue.Length);
				Marshal.Copy(this.salt, 0, intPtr2, this.salt.Length);
				SafariPassword.DATA_BLOB data_BLOB = default(SafariPassword.DATA_BLOB);
				data_BLOB.cbData = this.m_PasswordValue.Length;
				data_BLOB.pbData = intPtr;
				SafariPassword.DATA_BLOB data_BLOB2 = default(SafariPassword.DATA_BLOB);
				data_BLOB2.cbData = this.salt.Length;
				data_BLOB2.pbData = intPtr2;
				SafariPassword.DATA_BLOB data_BLOB3 = default(SafariPassword.DATA_BLOB);
				SafariPassword.CryptUnprotectData(ref data_BLOB, IntPtr.Zero, ref data_BLOB2, IntPtr.Zero, IntPtr.Zero, 0, ref data_BLOB3);
				int len = Marshal.ReadInt32(data_BLOB3.pbData);
				IntPtr ptr = new IntPtr(checked(data_BLOB3.pbData.ToInt64() + 4L));
				string result = Marshal.PtrToStringAnsi(ptr, len);
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
				return result;
			}
		}

		// Token: 0x040001B5 RID: 437
		private string m_Server;

		// Token: 0x040001B6 RID: 438
		private string m_UserName;

		// Token: 0x040001B7 RID: 439
		private byte[] salt;

		// Token: 0x040001B8 RID: 440
		private byte[] m_PasswordValue;

		// Token: 0x0200004C RID: 76
		private struct DATA_BLOB
		{
			// Token: 0x040001B9 RID: 441
			public int cbData;

			// Token: 0x040001BA RID: 442
			public IntPtr pbData;
		}
	}
}
